import { FC, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import { getSale, deleteSala } from '../api/salaApi';
import { Sala } from '../types/sala';
import { ROUTES } from '../constants/routes';
import { Loading } from '../components/Loading';
import { getErrorMessage } from '../utils/errorUtils';

export const SalaList: FC = () => {
    const [sale, setSale] = useState<Sala[] | null>(null);
    const [loading, setLoading] = useState(true);

    const loadSale = () => {
        setLoading(true);
        getSale()
            .then(setSale)
            .finally(() => setLoading(false));
    };

    useEffect(() => {
        loadSale();
    }, []);

    const handleDelete = (id: number) => {
        deleteSala(id)
            .then(() => {
                toast.success('Sala je obrisana.');
                loadSale();
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri brisanju sale.')));
    };

    if (loading) return <Loading />;

    return (
        <div>
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-medium">Sale</h1>
                <Link to={ROUTES.SALA_CREATE} className="bg-indigo-600 hover:bg-indigo-500 px-4 py-2 rounded-md text-sm font-medium transition-colors">
                    + Dodaj salu
                </Link>
            </div>

            {sale?.length === 0 && (
                <p className="text-slate-400 text-center py-16">Nema dodatih sala.</p>
            )}

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                {sale?.map(s => (
                    <div key={s.id} className="bg-slate-800 border border-slate-700 rounded-xl p-5 flex flex-col gap-2">
                        <div className="flex justify-between items-start">
                            <span className="text-xs bg-indigo-900 text-indigo-300 px-2.5 py-1 rounded-md">
                                {s.tip}
                            </span>
                        </div>
                        <p className="text-base font-medium text-white mt-1">{s.naziv}</p>
                        <p className="text-sm text-slate-400">{s.kapacitet} mesta</p>
                        <button
                            onClick={() => handleDelete(s.id)}
                            className="text-sm border border-red-800 text-red-400 hover:bg-red-900/30 px-3 py-1.5 rounded-md transition-colors mt-2"
                        >
                            Obrisi
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};