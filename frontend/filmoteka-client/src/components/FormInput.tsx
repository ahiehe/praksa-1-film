import { FC, InputHTMLAttributes } from 'react';

interface FormInputProps extends InputHTMLAttributes<HTMLInputElement> {
    label: string;
    error?: string;
}

export const FormInput: FC<FormInputProps> = ({ label, error, ...props }) => {
    return (
        <div className="flex flex-col gap-1.5">
            <label className="text-sm text-slate-300">{label}</label>
            <input
                {...props}
                className="bg-slate-800 border border-slate-700 rounded-md px-3 py-2 text-white text-sm focus:outline-none focus:border-indigo-500 transition-colors"
            />
            {error && <p className="text-xs text-red-400">{error}</p>}
        </div>
    );
};