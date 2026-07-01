import { FC, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { getReziseri, createReziser, deleteReziser } from '../api/reziserApi';
import { CreateReziserDTO } from '../types/dto';
import { FormInput } from '../components/FormInput';
import { Loading } from '../components/Loading';
import { getErrorMessage } from '../utils/errorUtils';
import { Reziser } from '../types/reziser';

const EMPTY: CreateReziserDTO = {
    ime: '',
    prezime: ''
};

export const ReziserList: FC = () => {
    const [reziseri, setReziseri] = useState<Reziser[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [form, setForm] = useState<CreateReziserDTO>(EMPTY);
    const [submitting, setSubmitting] = useState(false);

    const loadReziseri = () => {
        setLoading(true);
        getReziseri()
            .then(setReziseri)
            .finally(() => setLoading(false));
    };

    useEffect(() => {
        loadReziseri();
    }, []);

    const handleCreate = () => {
        setSubmitting(true);
        createReziser(form)
            .then(() => {
                toast.success('Režiser je dodat.');
                setForm(EMPTY);
                loadReziseri();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri dodavanju režisera.')))
            .finally(() => setSubmitting(false));
    };

    const handleDelete = (id: number) => {
        deleteReziser(id)
            .then(() => {
                toast.success('Režiser je obrisan.');
                loadReziseri();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri brisanju režisera.')));
    };

    return (
        <div>
            <h1 className="text-2xl font-medium mb-6">Režiseri</h1>

            <div className="flex gap-6 items-start">
                <div className="flex-1 bg-slate-800 border border-slate-700 rounded-xl overflow-hidden">
                    {loading ? <Loading /> : (
                        reziseri?.length === 0 ? (
                            <p className="text-slate-400 text-center py-16">Nema dodatih režisera.</p>
                        ) : (
                            <div className="divide-y divide-slate-700">
                                {reziseri?.map(r => (
                                    <div key={r.id} className="flex items-center justify-between px-5 py-3">
                                        <div className="flex flex-col gap-0.5">
                                            <span className="text-slate-200 text-sm">{r.ime} {r.prezime}</span>
                                            {r.datumRodjenja && (
                                                <span className="text-slate-500 text-xs">
                                                    {new Date(r.datumRodjenja).toLocaleDateString('sr-RS')}
                                                </span>
                                            )}
                                        </div>
                                        <button
                                            onClick={() => handleDelete(r.id)}
                                            className="text-xs border border-red-800 text-red-400 hover:bg-red-900/30 px-3 py-1 rounded-md transition-colors"
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
                    <p className="text-base font-medium text-white">Dodaj režisera</p>
                    <FormInput
                        label="Ime"
                        type="text"
                        value={form.ime}
                        onChange={e => setForm(p => ({ ...p, ime: e.target.value }))}
                    />
                    <FormInput
                        label="Prezime"
                        type="text"
                        value={form.prezime}
                        onChange={e => setForm(p => ({ ...p, prezime: e.target.value }))}
                    />
                    <FormInput
                        label="Datum rođenja"
                        type="date"
                        value={form.datumRodjenja}
                        onChange={e => setForm(p => ({ ...p, datumRodjenja: e.target.value }))}
                    />
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