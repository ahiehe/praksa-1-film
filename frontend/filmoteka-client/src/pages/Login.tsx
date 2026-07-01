import { FC, useState } from 'react';
import { login } from '../api/authApi';
import { AuthCard } from '../components/AuthCard';
import { FormInput } from '../components/FormInput';
import { saveAuth } from '../utils/storage';
import { ROUTES } from '../constants/routes';
import { Login as LoginType } from '../types/auth';
import toast from 'react-hot-toast';
import { getErrorMessage } from '../utils/errorUtils';

export const Login: FC = () => {
    const [form, setForm] = useState<LoginType>({ usernameOrEmail: '', password: '' });
    const [loading, setLoading] = useState(false);

    const handleSubmit = () => {
        setLoading(true);
        login(form)
            .then(res => {
                saveAuth(res.token, res.username);
                window.location.href = ROUTES.HOME;
            })
            .catch(err => toast.error(getErrorMessage(err)))
            .finally(() => setLoading(false));
    };

    return (
        <AuthCard
            title="Prijava"
            submitLabel="Prijavi se"
            bottomText="Nemate nalog?"
            bottomLinkText="Registrujte se"
            bottomLinkTo={ROUTES.REGISTER}
            onSubmit={handleSubmit}
            loading={loading}
        >
            <FormInput
                label="Korisničko ime ili email"
                type="text"
                value={form.usernameOrEmail}
                onChange={e => setForm(p => ({ ...p, usernameOrEmail: e.target.value }))}
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