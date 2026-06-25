import { FC, ReactNode } from 'react';
import { Link } from 'react-router-dom';

interface AuthCardProps {
    title: string;
    bottomText: string;
    bottomLinkText: string;
    bottomLinkTo: string;
    onSubmit: () => void;
    loading: boolean;
    error?: string;
    submitLabel: string;
    children: ReactNode;
}

export const AuthCard: FC<AuthCardProps> = ({
    title, bottomText, bottomLinkText, bottomLinkTo,
    onSubmit, loading, error, submitLabel, children
}) => {
    return (
        <div className="min-h-[80vh] flex items-center justify-center">
            <div className="bg-slate-800 border border-slate-700 rounded-xl p-8 w-full max-w-md flex flex-col gap-5">
                <h1 className="text-2xl font-medium text-white">{title}</h1>

                {error && (
                    <div className="bg-red-900/30 border border-red-800 text-red-400 text-sm px-4 py-2.5 rounded-md">
                        {error}
                    </div>
                )}

                {children}

                <button
                    onClick={onSubmit}
                    disabled={loading}
                    className="bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 py-2 rounded-md text-sm font-medium transition-colors mt-1"
                >
                    {loading ? 'Učitavanje...' : submitLabel}
                </button>

                <p className="text-sm text-slate-400 text-center">
                    {bottomText}{' '}
                    <Link to={bottomLinkTo} className="text-indigo-400 hover:text-indigo-300 transition-colors">
                        {bottomLinkText}
                    </Link>
                </p>
            </div>
        </div>
    );
};