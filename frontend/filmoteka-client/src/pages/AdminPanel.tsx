// pages/AdminPanel.tsx
import { FC } from 'react';
import { Link } from 'react-router-dom';
import { ROUTES } from '../constants/routes';

interface AdminCardProps {
    title: string;
    description: string;
    to: string;
}

const AdminCard: FC<AdminCardProps> = ({ title, description, to }) => (
    <Link
        to={to}
        className="bg-slate-800 border border-slate-700 rounded-xl p-6 flex flex-col gap-2 hover:border-indigo-500 transition-colors"
    >
        <p className="text-lg font-medium text-white">{title}</p>
        <p className="text-sm text-slate-400">{description}</p>
    </Link>
);

export const AdminPanel: FC = () => {
    return (
        <div>
            <h1 className="text-2xl font-medium mb-6">Admin panel</h1>

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                <AdminCard
                    title="Filmovi"
                    description="Dodaj, izmeni ili obriši filmove"
                    to={ROUTES.HOME}
                />
                <AdminCard
                    title="Termini"
                    description="Upravljaj projekcijama"
                    to={ROUTES.TERMINI}
                />
                <AdminCard
                    title="Korisnici"
                    description="Upravljaj listom korisnika"
                    to={ROUTES.USER_LIST}
                />
                <AdminCard
                    title="Sale"
                    description="Dodaj ili obriši bioskopske sale"
                    to={ROUTES.SALA_LIST}
                />
                <AdminCard
                    title="Žanrovi"
                    description="Upravljaj listom žanrova"
                    to={ROUTES.ZANR_LIST}
                />
                <AdminCard
                    title="Režiseri"
                    description="Upravljaj listom režisera"
                    to={ROUTES.REZISER_LIST}
                />
            </div>
        </div>
    );
};