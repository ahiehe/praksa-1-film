import { FC, useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import { getUserById, updateUser } from '../api/userApi';
import { FormInput } from '../components/FormInput';
import { Loading } from '../components/Loading';
import { ROUTES } from '../constants/routes';
import { getErrorMessage } from '../utils/errorUtils';
import { Role } from '../types/user';
import { UpdateUserDTO } from '../types/dto';

const ROLES: Role[] = ['Admin', 'Employee', 'User'];

export const UserEdit: FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [form, setForm] = useState<UpdateUserDTO>({});
    const [loading, setLoading] = useState(true);
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        getUserById(Number(id))
            .then(user => setForm({
                username: user.username,
                email: user.email,
                role: user.role,
                password: '',
            }))
            .finally(() => setLoading(false));
    }, [id]);

    const handleSubmit = () => {
        setSubmitting(true);
        updateUser(Number(id), {
            ...form,
            password: form.password || undefined,
        })
            .then(() => {
                toast.success('Korisnik je izmenjen.');
                navigate(ROUTES.USER_LIST);
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri izmeni korisnika.')))
            .finally(() => setSubmitting(false));
    };

    if (loading) return <Loading />;

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.USER_LIST} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                <h1 className="text-xl font-medium">Izmeni korisnika</h1>
                <div className="w-16" />
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8 flex flex-col gap-5">
                <FormInput
                    label="Korisničko ime"
                    type="text"
                    value={form.username ?? ''}
                    onChange={e => setForm(p => ({ ...p, username: e.target.value }))}
                />
                <FormInput
                    label="Email"
                    type="email"
                    value={form.email ?? ''}
                    onChange={e => setForm(p => ({ ...p, email: e.target.value }))}
                />
                <FormInput
                    label="Nova lozinka (ostavite prazno ako ne menjate)"
                    type="password"
                    value={form.password ?? ''}
                    onChange={e => setForm(p => ({ ...p, password: e.target.value }))}
                />
                <div className="flex flex-col gap-1.5">
                    <label className="text-sm text-slate-300">Uloga</label>
                    <select
                        value={form.role ?? 'User'}
                        onChange={e => setForm(p => ({ ...p, role: e.target.value as Role }))}
                        className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                    >
                        {ROLES.map(r => (
                            <option key={r} value={r}>{r}</option>
                        ))}
                    </select>
                </div>

                <button
                    onClick={handleSubmit}
                    disabled={submitting}
                    className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors mt-1"
                >
                    {submitting ? 'Čuvanje...' : 'Sačuvaj izmene'}
                </button>
            </div>
        </div>
    );
};