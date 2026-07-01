import { FC, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { getZanrovi, createZanr, deleteZanr } from '../api/zanrApi';
import { CreateZanrDTO } from '../types/dto';
import { FormInput } from '../components/FormInput';
import { Loading } from '../components/Loading';
import { getErrorMessage } from '../utils/errorUtils';
import { Zanr } from '../types/zanr';

export const ZanrList: FC = () => {
    const [zanrovi, setZanrovi] = useState<Zanr[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [form, setForm] = useState<CreateZanrDTO>({ naziv: '' });
    const [submitting, setSubmitting] = useState(false);

    const loadZanrovi = () => {
        setLoading(true);
        getZanrovi()
            .then(setZanrovi)
            .finally(() => setLoading(false));
    };

    useEffect(() => {
        loadZanrovi();
    }, []);

    const handleCreate = () => {
        setSubmitting(true);
        createZanr(form)
            .then(() => {
                toast.success('Žanr je dodat.');
                setForm({ naziv: '' });
                loadZanrovi();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri dodavanju žanra.')))
            .finally(() => setSubmitting(false));
    };

    const handleDelete = (id: number) => {
        deleteZanr(id)
            .then(() => {
                toast.success('Žanr je obrisan.');
                loadZanrovi();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri brisanju žanra.')));
    };

    return (
        <div>
            <h1 className="text-2xl font-medium mb-6">Žanrovi</h1>

            <div className="flex gap-6 items-start">
                <div className="flex-1 bg-slate-800 border border-slate-700 rounded-xl overflow-hidden">
                    {loading ? <Loading /> : (
                        zanrovi?.length === 0 ? (
                            <p className="text-slate-400 text-center py-16">Nema dodatih žanrova.</p>
                        ) : (
                            <div className="divide-y divide-slate-700">
                                {zanrovi?.map(z => (
                                    <div key={z.id} className="flex items-center justify-between px-5 py-3">
                                        <span className="text-slate-200 text-sm">{z.naziv}</span>
                                        <button
                                            onClick={() => handleDelete(z.id)}
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
                    <p className="text-base font-medium text-white">Dodaj žanr</p>
                    <FormInput
                        label="Naziv"
                        type="text"
                        value={form.naziv}
                        onChange={e => setForm({ naziv: e.target.value })}
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