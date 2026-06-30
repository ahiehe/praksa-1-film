import { FC } from 'react';
import { Link } from 'react-router-dom';
import { TerminInfo } from '../types/termin';
import { ROUTES } from '../constants/routes';

interface TerminCardProps {
    termin: TerminInfo;
}

export const TerminCard: FC<TerminCardProps> = ({ termin }) => {
    return (
        <Link
            to={ROUTES.TERMIN_DETAILS(termin.id)}
            className="bg-slate-800 border border-slate-700 rounded-xl p-5 flex flex-col gap-2 hover:border-slate-500 transition-colors"
        >
            <p className="text-base font-medium text-white">Film: {termin.nazivFilma}</p>
            <p className="text-sm text-slate-400">Sala: {termin.nazivSale}</p>
            <p className="text-sm text-slate-400">
                Pocetak: {new Date(termin.pocetakProjekcije).toLocaleString('sr-RS')}
            </p>
            <p className="text-xs text-slate-500 mt-1">
                {termin.brojDostupnihMesta} dostupnih mesta
            </p>
        </Link>
    );
};