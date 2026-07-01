import { FC, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getActiveTermini } from '../api/terminApi';
import { TerminInfo } from '../types/termin';
import { ROUTES } from '../constants/routes';
import { Loading } from '../components/Loading';
import { isUserAdmin, isUserEmployee } from '../utils/storage';
import { TerminCard } from '../components/TerminCard';

export const TerminList: FC = () => {
    const [termini, setTermini] = useState<TerminInfo[] | null>(null);
    const [loading, setLoading] = useState(true);

    const canManage = isUserAdmin() || isUserEmployee();

    useEffect(() => {
        getActiveTermini()
            .then(setTermini)
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <Loading />;

    return (
        <div>
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-medium">Projekcije</h1>
                {canManage && (
                    <Link to={ROUTES.TERMIN_CREATE} className="bg-indigo-600 hover:bg-indigo-500 px-4 py-2 rounded-md text-sm font-medium transition-colors">
                        + Dodaj termin
                    </Link>
                )}
            </div>

            {termini?.length === 0 && (
                <p className="text-slate-400 text-center py-16">Nema aktivnih projekcija.</p>
            )}

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                {termini?.map(t => (
                    <TerminCard key={t.id} termin={t} />
                ))}
            </div>
        </div>
    );
};