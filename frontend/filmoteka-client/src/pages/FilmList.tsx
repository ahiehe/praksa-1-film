import { useEffect, useState } from 'react';
import { ROUTES } from '../constants/routes';
import { Link } from 'react-router-dom';
import { FilmQueryDTO, PaginatedFilmsDTO } from '../types/dto';
import { Zanr } from '../types/film';
import { deleteFilm, getPaginatedFilms, getZanrovi } from '../api/filmApi';
import { FilmCard } from '../components/FilmCard';
import { Pagination } from '../components/Pagination';
import { Loading } from '../components/Loading';
import { isUserAdmin } from '../utils/storage';

export default function FilmList() {
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState<PaginatedFilmsDTO | null>(null);
    const [zanrovi, setZanrovi] = useState<Zanr[]>([]);
    const [searchInput, setSearchInput] = useState('');
    const [query, setQuery] = useState<FilmQueryDTO>({ page: 1 });

    const isAdmin = isUserAdmin();

    useEffect(() => {
        getZanrovi().then(setZanrovi);
    }, []);

    useEffect(() => {
        const timer = setTimeout(() => {
            setQuery(p => ({ ...p, page: 1, search: searchInput || undefined }));
        }, 600);
        return () => clearTimeout(timer);
    }, [searchInput]);

    useEffect(() => {
        setLoading(true);
        getPaginatedFilms(query)
            .then(res => setData(res))
            .finally(() => setLoading(false));
    }, [query]);

    const handleDelete = (id: number) => {
        deleteFilm(id).then(() => {
            getPaginatedFilms(query).then(res => setData(res));
        });
    };

    return (
        <div>
            {isAdmin &&
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-medium">Filmovi</h1>
                <Link to={ROUTES.CREATE} className="bg-indigo-600 hover:bg-indigo-500 px-4 py-2 rounded-md text-sm font-medium transition-colors">
                    + Dodaj film
                </Link>
            </div>
            }

            <div className="flex flex-col sm:flex-row gap-3 mb-6">
                <input
                    type="text"
                    placeholder="Pretraži filmove..."
                    value={searchInput}
                    onChange={e => setSearchInput(e.target.value)}
                    className="flex-1 bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                />
                <select
                    value={query.zanrId ?? ''}
                    onChange={e => setQuery(p => ({ ...p, page: 1, zanrId: e.target.value ? Number(e.target.value) : undefined }))}
                    className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                >
                    <option value="">Svi žanrovi</option>
                    {zanrovi.map(z => (
                        <option key={z.id} value={z.id}>{z.naziv}</option>
                    ))}
                </select>
                <input
                    type="number"
                    placeholder="Od godine"
                    value={query.godinaOd ?? ''}
                    onChange={e => setQuery(p => ({ ...p, page: 1, godinaOd: e.target.value ? Number(e.target.value) : undefined }))}
                    className="w-32 bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                />
                <input
                    type="number"
                    placeholder="Do godine"
                    value={query.godinaDo ?? ''}
                    onChange={e => setQuery(p => ({ ...p, page: 1, godinaDo: e.target.value ? Number(e.target.value) : undefined }))}
                    className="w-32 bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                />
                <label className="flex items-center gap-2 text-sm text-slate-300 cursor-pointer">
                    <input
                        type="checkbox"
                        checked={query.uBioskopima ?? false}
                        onChange={e => setQuery(p => ({ ...p, page: 1, uBioskopima: e.target.checked || undefined }))}
                        className="accent-indigo-500"
                    />
                    U bioskopu
                </label>
            </div>

            {loading ? <Loading /> : (
                <>
                    {data?.filmovi.length === 0 && (
                        <p className="text-slate-400 text-center py-16">Nema filmova koji odgovaraju pretrazi.</p>
                    )}
                    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                        {data?.filmovi.map(film => (
                            <FilmCard key={film.id} film={film} onDelete={handleDelete} isAdmin={isAdmin} />
                        ))}
                    </div>
                    {data && data.ukupnoStranica > 1 && (
                        <Pagination
                            trenutna={data.trenutnaStranica}
                            ukupno={data.ukupnoStranica}
                            onChange={(newPage: number) => setQuery(p => ({ ...p, page: newPage }))}
                        />
                    )}
                </>
            )}
        </div>
    );
}