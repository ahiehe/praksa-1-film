import { FC } from "react";
import { Film } from "../types/film";
import { Link } from "react-router-dom";
import { ROUTES } from "../constants/routes";
import { isUBioskopima } from "../utils/filmUtils";

interface FilmCardProps {
    film: Film;
    onDelete: (id: number) => void;
}

export const FilmCard: FC<FilmCardProps> = ({ film, onDelete }) => {
    const uBioskopima = isUBioskopima(film);

    return (
        <div className="bg-slate-800 border border-slate-700 rounded-xl p-5 flex flex-col gap-2">
            <div className="flex justify-between items-start">
                <div className="flex gap-2">
                    <span className="text-xs bg-indigo-900 text-indigo-300 px-2.5 py-1 rounded-md">
                        {film.zanr.naziv}
                    </span>
                    {uBioskopima && (
                        <span className="text-xs bg-green-900 text-green-300 px-2.5 py-1 rounded-md">
                            U bioskopu
                        </span>
                    )}
                </div>
                <span className="text-sm text-slate-400">{film.godinaIzdanja}</span>
            </div>

            <p className="text-base font-medium text-white mt-1">{film.naziv}</p>

            <p className="text-sm text-slate-400 leading-relaxed line-clamp-2">{film.opis}</p>

            <div className="flex items-center gap-1.5 mt-1">
                <span className="text-slate-500 text-xs">
                    {film.reziseri.map(r => `${r.ime} ${r.prezime}`).join(', ')}
                </span>
            </div>

            {film.pocetakPrikazivanja && film.krajPrikazivanja && (
                <div className="text-xs text-slate-500 mt-1">
                    {new Date(film.pocetakPrikazivanja).toLocaleDateString('sr-RS')} – {new Date(film.krajPrikazivanja).toLocaleDateString('sr-RS')}
                </div>
            )}

            <div className="flex gap-2 mt-2">
                <Link to={ROUTES.DETAILS(film.id)} className="flex-1 text-center text-sm border border-slate-600 hover:border-slate-400 px-3 py-1.5 rounded-md transition-colors">
                    Detalji
                </Link>
                <Link to={ROUTES.EDIT(film.id)} className="flex-1 text-center text-sm border border-slate-600 hover:border-slate-400 px-3 py-1.5 rounded-md transition-colors">
                    Izmeni
                </Link>
                <button
                    onClick={() => onDelete(film.id)}
                    className="text-sm border border-red-800 text-red-400 hover:bg-red-900/30 px-3 py-1.5 rounded-md transition-colors"
                >
                    Obrisi
                </button>
            </div>
        </div>
    );
}