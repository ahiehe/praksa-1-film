import { FC, useEffect, useState } from 'react';
import { getZanrovi, getReziseri } from '../api/filmApi';
import { Zanr,  ReziserOption } from '../types/film';
import { CreateFilmDTO } from '../types/dto';
import { FormInput } from './FormInput';

interface FilmFormProps {
    initial?: CreateFilmDTO;
    onSubmit: (dto: CreateFilmDTO) => void;
    loading: boolean;
    submitLabel: string;
}

const EMPTY: CreateFilmDTO = {
    naziv: '',
    godinaIzdanja: new Date().getFullYear(),
    zanrId: 0,
    opis: '',
    izabraniReziseri: [],
    pocetakPrikazivanja: null,
    krajPrikazivanja: null,
};

export const FilmForm: FC<FilmFormProps> = ({ initial, onSubmit, loading, submitLabel }) => {
    const [form, setForm] = useState<CreateFilmDTO>(initial ?? EMPTY);
    const [zanrovi, setZanrovi] = useState<Zanr[]>([]);
    const [reziseri, setReziseri] = useState<ReziserOption[]>([]);

    useEffect(() => {
        getZanrovi().then(data => {
            setZanrovi(data);
            setForm(p => ({ ...p, zanrId: p.zanrId === 0 ? data[0]?.id ?? 0 : p.zanrId }));
        });
        getReziseri().then(setReziseri);
    }, []);

    const toggleReziser = (id: number) => {
        setForm(p => ({
            ...p,
            izabraniReziseri: p.izabraniReziseri.includes(id)
                ? p.izabraniReziseri.filter(r => r !== id)
                : [...p.izabraniReziseri, id],
        }));
    };

    return (
        <div className="flex flex-col gap-5">

            <FormInput
                label="Naziv"
                type="text"
                value={form.naziv}
                onChange={e => setForm(p => ({ ...p, naziv: e.target.value }))}
            />

            <FormInput
                label="Godina izdanja"
                type="number"
                value={form.godinaIzdanja}
                onChange={e => setForm(p => ({ ...p, godinaIzdanja: Number(e.target.value) }))}
            />

            <div className="flex flex-col gap-1.5">
                <label className="text-sm text-slate-300">Žanr</label>
                <select
                    value={form.zanrId}
                    onChange={e => setForm(p => ({ ...p, zanrId: Number(e.target.value) }))}
                    className="bg-slate-800 text-white border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                >
                    <option value={0} disabled>Izaberi žanr</option>
                    {zanrovi.map(z => (
                        <option key={z.id} value={z.id}>{z.naziv}</option>
                    ))}
                </select>
            </div>

            <div className="flex flex-col gap-1.5">
                <label className="text-sm text-slate-300">Opis</label>
                <textarea
                    value={form.opis}
                    onChange={e => setForm(p => ({ ...p, opis: e.target.value }))}
                    rows={4}
                    className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors resize-none"
                />
            </div>

            <div className="flex flex-col gap-2">
                <label className="text-sm text-slate-300">Režiseri</label>
                <div className="flex flex-col gap-2">
                    {reziseri.map(r => (
                        <label key={r.id} className="flex items-center gap-3 bg-slate-800 border border-slate-700 px-4 py-2.5 rounded-md cursor-pointer hover:border-slate-500 transition-colors">
                            <input
                                type="checkbox"
                                checked={form.izabraniReziseri.includes(r.id)}
                                onChange={() => toggleReziser(r.id)}
                                className="accent-indigo-500"
                            />
                            <span className="text-sm text-slate-200">{r.ime} {r.prezime}</span>
                        </label>
                    ))}
                </div>
            </div>

            <div className="flex gap-4">
                <FormInput
                    label="Početak prikazivanja"
                    type="date"
                    value={form.pocetakPrikazivanja ?? ''}
                    onChange={e => setForm(p => ({ ...p, pocetakPrikazivanja: e.target.value || null }))}
                />
                <FormInput
                    label="Kraj prikazivanja"
                    type="date"
                    value={form.krajPrikazivanja ?? ''}
                    onChange={e => setForm(p => ({ ...p, krajPrikazivanja: e.target.value || null }))}
                />
            </div>

            <button
                onClick={() => onSubmit(form)}
                disabled={loading}
                className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors mt-1"
            >
                {loading ? 'Učitavanje...' : submitLabel}
            </button>
        </div>
    );
};