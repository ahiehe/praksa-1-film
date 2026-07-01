import axios from 'axios';

export const getErrorMessage = (err: unknown, fallback = 'Greška.'): string => {
    if (axios.isAxiosError(err)) {
        const data = err.response?.data;
        const firstError = data?.errors
            ? Object.values(data.errors).flat()[0] as string
            : null;
        return firstError ?? data?.message ?? fallback;
    }
    return fallback;
};