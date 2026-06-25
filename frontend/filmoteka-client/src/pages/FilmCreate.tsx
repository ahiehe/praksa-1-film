import { FC, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { createFilm } from '../api/filmApi';
import { CreateFilmDTO } from '../types/dto';
import { FilmForm } from '../components/FilmForm';
import { ROUTES } from '../constants/routes';
import axios from 'axios';

export const FilmCreate: FC = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleSubmit = (dto: CreateFilmDTO) => {
        setError('');
        setLoading(true);
        createFilm(dto)
            .then((res) => navigate(ROUTES.DETAILS(res.id)))
            .catch(err => setError(axios.isAxiosError(err)
                ? err.response?.data?.message ?? 'Greška pri kreiranju.'
                : 'Greška.'
            ))
            .finally(() => setLoading(false));
    };

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.HOME} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                <h1 className="text-xl font-medium">Dodaj film</h1>
                <div className="w-16" />
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8">
                <FilmForm
                    onSubmit={handleSubmit}
                    loading={loading}
                    error={error}
                    submitLabel="Dodaj film"
                />
            </div>
        </div>
    );
};