import { useEffect, useState } from 'react';
import { ROUTES } from '../constants/routes';
import { Link } from 'react-router-dom';
import { PaginatedFilmsDTO } from '../types/dto';
import { deleteFilm, getPaginatedFilms } from '../api/filmApi';
import { FilmCard } from '../components/FilmCard';
import { Pagination } from '../components/Pagination';
import { Loading } from '../components/Loading';

export default function FilmList() {
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState<PaginatedFilmsDTO | null>();
    const [page, setPage] = useState(1);

    useEffect(() => {
        getPaginatedFilms(page)
            .then(res => setData(res))
            .finally(() => setLoading(false))
    }, [page]);

    const handleDelete = (id: number) => {
        deleteFilm(id).then(() => {
            getPaginatedFilms(page).then(res => setData(res))
        });     
    }

    if (loading){
        return <Loading />
    }

    if (!data){
        return null
    }

    return (
        <div>
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-medium">Filmovi</h1>
                <Link to={ROUTES.CREATE} className="bg-indigo-600 hover:bg-indigo-500 px-4 py-2 rounded-md text-sm font-medium transition-colors">
                    + Dodaj film
                </Link>
            </div>


            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                {data.filmovi.map(film => (
                    <FilmCard key={film.id} film={film} onDelete={handleDelete} />
                ))}
            </div>

            <Pagination trenutna={data.trenutnaStranica} ukupno={data.ukupnoStranica} onChange={setPage} />
        </div>
    );
}