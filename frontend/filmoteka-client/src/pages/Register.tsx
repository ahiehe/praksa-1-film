import { FC, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { register } from '../api/authApi';
import { AuthCard } from '../components/AuthCard';
import { FormInput } from '../components/FormInput';
import { saveAuth } from '../utils/storage';
import { ROUTES } from '../constants/routes';
import { Register as RegisterType } from '../types/auth';
import toast from 'react-hot-toast';
import axios from 'axios';
import { getErrorMessage } from '../utils/errorUtils';

export const Register: FC = () => {
    const navigate = useNavigate();
    const [form, setForm] = useState<RegisterType>({ username: '', email: '', password: '' });
    const [loading, setLoading] = useState(false);

    const handleSubmit = () => {
        setLoading(true);
        register(form)
            .then(res => {
                saveAuth(res.token, res.username);
                window.location.href = ROUTES.HOME;
            })
            .catch(err => toast.error(getErrorMessage(err)))
            .finally(() => setLoading(false));
    };

    return (
        <AuthCard
            title="Registracija"
            submitLabel="Registruj se"
            bottomText="Već imate nalog?"
            bottomLinkText="Prijavite se"
            bottomLinkTo={ROUTES.LOGIN}
            onSubmit={handleSubmit}
            loading={loading}
        >
            <FormInput
                label="Korisničko ime"
                type="text"
                value={form.username}
                onChange={e => setForm(p => ({ ...p, username: e.target.value }))}
            />
            <FormInput
                label="Email"
                type="email"
                value={form.email}
                onChange={e => setForm(p => ({ ...p, email: e.target.value }))}
            />
            <FormInput
                label="Lozinka"
                type="password"
                value={form.password}
                onChange={e => setForm(p => ({ ...p, password: e.target.value }))}
            />
        </AuthCard>
    );
};