import { FC, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import { createSala } from '../api/salaApi';
import { TipSale } from '../types/sala';
import { FormInput } from '../components/FormInput';
import { ROUTES } from '../constants/routes';
import { getErrorMessage } from '../utils/errorUtils';
import { CreateSalaDTO } from '../types/dto';

const TIP_SALE_OPTIONS: { value: TipSale; label: string }[] = [
    { value: 'Standard', label: 'Standard' },
    { value: 'IMAX', label: 'IMAX' },
    { value: '_3D', label: '3D' },
];

const EMPTY: CreateSalaDTO = {
    naziv: '',
    kapacitet: 50,
    tip: "Standard",
};

export const SalaCreate: FC = () => {
    const navigate = useNavigate();
    const [form, setForm] = useState<CreateSalaDTO>(EMPTY);
    const [loading, setLoading] = useState(false);

    const handleSubmit = () => {
        setLoading(true);
        createSala(form)
            .then(() => {
                toast.success('Sala je uspešno kreirana!');
                navigate(ROUTES.HOME);
            })
            .catch(err => toast.error(getErrorMessage(err, 'Greška pri kreiranju sale.')))
            .finally(() => setLoading(false));
    };

    return (
        <div className="max-w-2xl mx-auto py-8 flex flex-col gap-6">
            <div className="flex items-center justify-between">
                <Link to={ROUTES.HOME} className="text-slate-400 hover:text-white text-sm transition-colors">
                    ← Nazad
                </Link>
                <h1 className="text-xl font-medium">Dodaj salu</h1>
                <div className="w-16" />
            </div>

            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8 flex flex-col gap-5">
                <FormInput
                    label="Naziv"
                    type="text"
                    value={form.naziv}
                    onChange={e => setForm(p => ({ ...p, naziv: e.target.value }))}
                />

                <FormInput
                    label="Kapacitet"
                    type="number"
                    value={form.kapacitet}
                    onChange={e => setForm(p => ({ ...p, kapacitet: Number(e.target.value) }))}
                />

                <div className="flex flex-col gap-1.5">
                    <label className="text-sm text-slate-300">Tip sale</label>
                    <select
                        value={form.tip}
                        onChange={e => setForm(p => ({ ...p, tip: e.target.value as TipSale }))}
                        className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
                    >
                        {TIP_SALE_OPTIONS.map(opt => (
                            <option key={opt.value} value={opt.value}>{opt.label}</option>
                        ))}
                    </select>
                </div>

                <button
                    onClick={handleSubmit}
                    disabled={loading}
                    className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors mt-1"
                >
                    {loading ? 'Učitavanje...' : 'Dodaj salu'}
                </button>
            </div>
        </div>
    );
};