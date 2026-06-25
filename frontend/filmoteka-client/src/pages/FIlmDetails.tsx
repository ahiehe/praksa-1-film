import { FC, useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { getFilmById, deleteFilm } from '../api/filmApi';
import { Film } from '../types/film';
import { ROUTES } from '../constants/routes';
import { Loading } from '../components/Loading';

export const FilmDetails: FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [film, setFilm] = useState<Film | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        getFilmById(Number(id))
            .then(res => setFilm(res))
            .catch(() => setError('Film nije pronađen.'))
            .finally(() => setLoading(false));
    }, [id]);

    const handleDelete = () => {
        if (!film) return;
        deleteFilm(film.id).then(() => navigate(ROUTES.HOME));
    };

    if (loading) return <Loading />;

    if (error) return (
        <div className="flex flex-col items-center justify-center py-24 gap-4">
            <p className="text-red-400">{error}</p>
            <Link to={ROUTES.HOME} className="text-indigo-400 hover:text-indigo-300 text-sm transition-colors">
                ← Nazad na listu
            </Link>
        </div>
    );

    if (!film) return null;

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.HOME} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                <div className="flex gap-2">
                    <Link
                        to={ROUTES.EDIT(film.id)}
                        className="text-sm border border-slate-600 hover:border-slate-400 px-4 py-1.5 rounded-md transition-colors"
                    >
                        Izmeni
                    </Link>
                    <button
                        onClick={handleDelete}
                        className="text-sm border border-red-800 text-red-400 hover:bg-red-900/30 px-4 py-1.5 rounded-md transition-colors"
                    >
                        Obriši
                    </button>
                </div>
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8 flex flex-col gap-5">
                <div className="flex items-start justify-between gap-4">
                    <h1 className="text-2xl font-medium text-white">{film.naziv}</h1>
                    <span className="text-sm bg-indigo-900 text-indigo-300 px-3 py-1 rounded-md shrink-0">
                        {film.zanr.naziv}
                    </span>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Opis</p>
                    <p className="text-slate-300 leading-relaxed">{film.opis}</p>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Godina izdanja</p>
                    <p className="text-slate-300">{film.godinaIzdanja}</p>
                </div>

                <div className="flex flex-col gap-1">
                    <p className="text-xs text-slate-500 uppercase tracking-wider">Režiseri</p>
                    <div className="flex flex-col gap-2 mt-1">
                        {film.reziseri.map(r => (
                            <div key={r.id} className="flex items-center justify-between bg-slate-700/50 px-4 py-2.5 rounded-md">
                                <span className="text-slate-200 text-sm">{r.ime} {r.prezime}</span>
                                <span className="text-slate-500 text-xs">
                                    {new Date(r.datumRodjenja).toLocaleDateString('sr-RS')}
                                </span>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
};