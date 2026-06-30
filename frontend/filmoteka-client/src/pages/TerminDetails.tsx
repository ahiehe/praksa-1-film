import { FC, useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import { getTerminById, reserveTermin, deleteTermin } from '../api/terminApi';
import { TerminDetails as TerminDetailsType } from '../types/termin';
import { ROUTES } from '../constants/routes';
import { Loading } from '../components/Loading';
import { isUserAdmin, isUserEmployee } from '../utils/storage';
import { getErrorMessage } from '../utils/errorUtils';

export const TerminDetails: FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [termin, setTermin] = useState<TerminDetailsType | null>(null);
    const [loading, setLoading] = useState(true);
    const [reserving, setReserving] = useState(false);

    const canManage = isUserAdmin() || isUserEmployee();

    useEffect(() => {
        getTerminById(Number(id))
            .then(setTermin)
            .finally(() => setLoading(false));
    }, [id]);

    const handleReserve = () => {
        setReserving(true);
        reserveTermin({ terminId: Number(id) })
            .then(() => {
                toast.success('Rezervacija uspešna!');
                setTermin(p => p ? { ...p, vecRezervisano: true, brojDostupnihMesta: p.brojDostupnihMesta - 1 } : p);
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri rezervaciji.')))
            .finally(() => setReserving(false));
    };

    const handleDelete = () => {
        deleteTermin(Number(id))
            .then(() => {
                toast.success('Termin je obrisan.');
                navigate(ROUTES.TERMINI);
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri brisanju.')));
    };

    if (loading) return <Loading />;
    if (!termin) return null;

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.TERMINI} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                {canManage && (
                    <button
                        onClick={handleDelete}
                        className="text-sm border border-red-800 text-red-400 hover:bg-red-900/30 px-4 py-1.5 rounded-md transition-colors"
                    >
                        Obriši termin
                    </button>
                )}
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8 flex flex-col gap-5">
                <div className="flex items-start justify-between gap-4">
                    <h1 className="text-2xl font-medium text-white">{termin.nazivFilma}</h1>
                    <span className="text-sm bg-indigo-900 text-indigo-300 px-3 py-1 rounded-md shrink-0">
                        {termin.zanrNaziv}
                    </span>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Opis</p>
                    <p className="text-slate-300 leading-relaxed">{termin.opisFilma}</p>
                </div>

                <div className="grid grid-cols-2 gap-4">
                    <div className="flex flex-col gap-1">
                        <p className="text-xs text-slate-500 uppercase tracking-wider">Godina izdanja</p>
                        <p className="text-slate-300">{termin.godinaIzdanja}</p>
                    </div>
                    <div className="flex flex-col gap-1">
                        <p className="text-xs text-slate-500 uppercase tracking-wider">Sala</p>
                        <p className="text-slate-300">{termin.nazivSale} ({termin.kapacitetSale} mesta)</p>
                    </div>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Vreme projekcije</p>
                    <p className="text-slate-300">
                        {new Date(termin.pocetakProjekcije).toLocaleString('sr-RS')} – {new Date(termin.krajProjekcije).toLocaleString('sr-RS')}
                    </p>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Režiseri</p>
                    <p className="text-slate-300">{termin.reziseri.join(', ')}</p>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Dostupna mesta</p>
                    <p className="text-slate-300">{termin.brojDostupnihMesta}</p>
                </div>

                {termin.vecRezervisano ? (
                    <div className="bg-green-900/30 border border-green-800 text-green-400 text-sm px-4 py-2.5 rounded-md text-center">
                        Već ste rezervisali ovaj termin
                    </div>
                ) : (
                    <button
                        onClick={handleReserve}
                        disabled={reserving || termin.brojDostupnihMesta <= 0}
                        className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors"
                    >
                        {termin.brojDostupnihMesta <= 0
                            ? 'Nema dostupnih mesta'
                            : reserving ? 'Učitavanje...' : 'Rezerviši'}
                    </button>
                )}
            </div>
        </div>
    );
};