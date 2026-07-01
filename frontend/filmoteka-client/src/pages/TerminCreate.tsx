import { FC, useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import { createTermin } from '../api/terminApi';
import { getSale } from '../api/salaApi';
import { getFilmOptions } from '../api/filmApi';
import { CreateTerminDTO } from '../types/dto';
import { Sala } from '../types/sala';
import { FilmOption } from '../types/film';
import { FormInput } from '../components/FormInput';
import { ROUTES } from '../constants/routes';
import { getErrorMessage } from '../utils/errorUtils';

const EMPTY: CreateTerminDTO = {
    salaId: 0,
    filmId: 0,
    pocetakProjekcije: '',
    krajProjekcije: '',
};

export const TerminCreate: FC = () => {
    const navigate = useNavigate();
    const [form, setForm] = useState<CreateTerminDTO>(EMPTY);
    const [sale, setSale] = useState<Sala[]>([]);
    const [filmovi, setFilmovi] = useState<FilmOption[]>([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        getSale().then(setSale);
        getFilmOptions().then(setFilmovi);
    }, []);

    const handleSubmit = () => {
        if (!form.pocetakProjekcije || !form.krajProjekcije) {
            toast.error('Unesite vreme početka i kraja projekcije.');
            return;
        }

        setLoading(true);
        createTermin(form)
            .then(() => {
                toast.success('Termin je uspešno kreiran!');
                navigate(ROUTES.TERMINI);
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri kreiranju termina.')))
            .finally(() => setLoading(false));
    };

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.TERMINI} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                <h1 className="text-xl font-medium">Dodaj termin</h1>
                <div className="w-16" />
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8 flex flex-col gap-5">
                <div className="flex flex-col gap-1.5">
                    <label className="text-sm text-slate-300">Film</label>
                    <select
                        value={form.filmId}
                        onChange={e => setForm(p => ({ ...p, filmId: Number(e.target.value) }))}
                        className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                    >
                        <option value={0} disabled>Izaberi film</option>
                        {filmovi.map(f => (
                            <option key={f.id} value={f.id}>{f.naziv}</option>
                        ))}
                    </select>
                </div>

                <div className="flex flex-col gap-1.5">
                    <label className="text-sm text-slate-300">Sala</label>
                    <select
                        value={form.salaId}
                        onChange={e => setForm(p => ({ ...p, salaId: Number(e.target.value) }))}
                        className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                    >
                        <option value={0} disabled>Izaberi salu</option>
                        {sale.map(s => (
                            <option key={s.id} value={s.id}>{s.naziv} ({s.tip}) - {s.kapacitet} mesta</option>
                        ))}
                    </select>
                </div>

                <div className="flex gap-4">
                    <FormInput
                        label="Početak projekcije"
                        type="datetime-local"
                        value={form.pocetakProjekcije}
                        onChange={e => setForm(p => ({ ...p, pocetakProjekcije: e.target.value }))}
                    />
                    <FormInput
                        label="Kraj projekcije"
                        type="datetime-local"
                        value={form.krajProjekcije}
                        onChange={e => setForm(p => ({ ...p, krajProjekcije: e.target.value }))}
                    />
                </div>

                <button
                    onClick={handleSubmit}
                    disabled={loading}
                    className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors mt-1"
                >
                    {loading ? 'Učitavanje...' : 'Dodaj termin'}
                </button>
            </div>
        </div>
    );
};