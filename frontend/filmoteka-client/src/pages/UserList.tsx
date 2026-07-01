import { FC, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { getUsers, createUser, deleteUser } from '../api/userApi';
import { UserInfo, Role } from '../types/user';
import { FormInput } from '../components/FormInput';
import { Loading } from '../components/Loading';
import { getErrorMessage } from '../utils/errorUtils';
import { CreateUserDTO } from '../types/dto';
import { Link } from 'react-router-dom';
import { ROUTES } from '../constants/routes';

const ROLES: Role[] = ['Admin', 'Employee', 'User'];
const roleColors: Record<Role, string> = {
    Admin: 'bg-red-900 text-red-300',
    Employee: 'bg-amber-900 text-amber-300',
    User: 'bg-slate-700 text-slate-300',
};

const EMPTY: CreateUserDTO = {
    username: '',
    email: '',
    password: '',
    role: 'User',
};

export const UserList: FC = () => {
    const [users, setUsers] = useState<UserInfo[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [form, setForm] = useState<CreateUserDTO>(EMPTY);
    const [submitting, setSubmitting] = useState(false);

    const loadUsers = () => {
        setLoading(true);
        getUsers()
            .then(setUsers)
            .finally(() => setLoading(false));
    };

    useEffect(() => {
        loadUsers();
    }, []);

    const handleCreate = () => {
        setSubmitting(true);
        createUser(form)
            .then(() => {
                toast.success('Korisnik je dodat.');
                setForm(EMPTY);
                loadUsers();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri dodavanju korisnika.')))
            .finally(() => setSubmitting(false));
    };

    const handleDelete = (id: number) => {
        deleteUser(id)
            .then(() => {
                toast.success('Korisnik je obrisan.');
                loadUsers();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri brisanju korisnika.')));
    };

    return (
        <div>
            <h1 className="text-2xl font-medium mb-6">Korisnici</h1>

            <div className="flex gap-6 items-start">
                <div className="flex-1 bg-slate-800 border border-slate-700 rounded-xl overflow-hidden">
                    {loading ? <Loading /> : (
                        users?.length === 0 ? (
                            <p className="text-slate-400 text-center py-16">Nema korisnika.</p>
                        ) : (
                            <div className="divide-y divide-slate-700">
                                {users?.map(u => (
                                    <div key={u.id} className="flex items-center justify-between px-5 py-3 gap-4">
                                        <div className="flex flex-col gap-0.5 flex-1 min-w-0">
                                            <div className="flex items-center gap-2">
                                                <span className="text-slate-200 text-sm font-medium">{u.username}</span>
                                                <span className={`text-xs px-2 py-0.5 rounded-md ${roleColors[u.role]}`}>
                                                    {u.role}
                                                </span>
                                            </div>
                                            <span className="text-slate-500 text-xs truncate">{u.email}</span>
                                        </div>
                                        <Link
                                            to={ROUTES.USER_EDIT(u.id)}
                                            className="text-xs border border-slate-600 hover:border-slate-400 px-3 py-1 rounded-md transition-colors shrink-0"
                                        >
                                            Izmeni
                                        </Link>
                                        <button
                                            onClick={() => handleDelete(u.id)}
                                            className="text-xs border border-red-800 text-red-400 hover:bg-red-900/30 px-3 py-1 rounded-md transition-colors shrink-0"
                                        >
                                            Obrisi
                                        </button>
                                    </div>
                                ))}
                            </div>
                        )
                    )}
                </div>

                <div className="w-72 sticky top-6 bg-slate-800 border border-slate-700 rounded-xl p-5 flex flex-col gap-4">
                    <p className="text-base font-medium text-white">Dodaj korisnika</p>
                    <FormInput
                        label="Korisničko ime"
                        type="text"
                        value={form.username}
                        onChange={e => setForm(p => ({ ...p, username: e.target.value }))}
                    />
                    <FormInput
                        label="Email"
                        type="email"
                        value={form.email}
                        onChange={e => setForm(p => ({ ...p, email: e.target.value }))}
                    />
                    <FormInput
                        label="Lozinka"
                        type="password"
                        value={form.password}
                        onChange={e => setForm(p => ({ ...p, password: e.target.value }))}
                    />
                    <div className="flex flex-col gap-1.5">
                        <label className="text-sm text-slate-300">Uloga</label>
                        <select
                            value={form.role}
                            onChange={e => setForm(p => ({ ...p, role: e.target.value as Role }))}
                            className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                        >
                            {ROLES.map(r => (
                                <option key={r} value={r}>{r}</option>
                            ))}
                        </select>
                    </div>
                    <button
                        onClick={handleCreate}
                        disabled={submitting}
                        className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors"
                    >
                        {submitting ? 'Dodavanje...' : 'Dodaj'}
                    </button>
                </div>
            </div>
        </div>
    );
};