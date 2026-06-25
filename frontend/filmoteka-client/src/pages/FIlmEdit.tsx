import { FC, useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { getFilmById, updateFilm } from '../api/filmApi';
import { CreateFilmDTO } from '../types/dto';
import { FilmForm } from '../components/FilmForm';
import { ROUTES } from '../constants/routes';
import { Loading } from '../components/Loading';
import axios from 'axios';

export const FilmEdit: FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [initial, setInitial] = useState<CreateFilmDTO | null>(null);
    const [loading, setLoading] = useState(false);
    const [fetchLoading, setFetchLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        getFilmById(Number(id))
            .then(film => {
                setInitial({
                    naziv: film.naziv,
                    godinaIzdanja: film.godinaIzdanja,
                    zanrId: film.zanrId ?? 0,
                    opis: film.opis,
                    izabraniReziseri: film.reziseri.map(r => r.id),
                });
            })
            .finally(() => setFetchLoading(false));
    }, [id]);

    const handleSubmit = (dto: CreateFilmDTO) => {
        setError('');
        setLoading(true);
        updateFilm(Number(id), dto)
            .then(() => navigate(ROUTES.DETAILS(Number(id))))
            .catch(err => setError(axios.isAxiosError(err)
                ? err.response?.data?.message ?? 'Greška pri izmeni.'
                : 'Greška.'
            ))
            .finally(() => setLoading(false));
    };

    if (fetchLoading) return <Loading />;
    if (!initial) return null;

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.HOME} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                <h1 className="text-xl font-medium">Izmeni film</h1>
                <div className="w-16" />
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8">
                <FilmForm
                    initial={initial}
                    onSubmit={handleSubmit}
                    loading={loading}
                    error={error}
                    submitLabel="Sačuvaj izmene"
                />
            </div>
        </div>
    );
};