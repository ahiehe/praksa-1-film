import { FC } from "react";

interface PaginationProps {
    trenutna: number;
    ukupno: number;
    onChange: (page: number) => void;
}

export const Pagination: FC<PaginationProps> = ({ trenutna, ukupno, onChange }) => {
    return (
        <div className="flex justify-center items-center gap-2 mt-8">
            <button
                onClick={() => onChange(trenutna - 1)}
                disabled={trenutna === 1}
                className="w-9 h-9 rounded-md border border-slate-700 text-slate-400 hover:border-slate-500 disabled:opacity-30 transition-colors"
            >
                ‹
            </button>
            {Array.from({ length: ukupno }, (_, i) => i + 1).map(page => (
                <button
                    key={page}
                    onClick={() => onChange(page)}
                    className={`w-9 h-9 rounded-md border text-sm transition-colors ${
                        page === trenutna
                            ? 'bg-indigo-600 border-indigo-600 text-white'
                            : 'border-slate-700 text-slate-400 hover:border-slate-500'
                    }`}
                >
                    {page}
                </button>
            ))}
            <button
                onClick={() => onChange(trenutna + 1)}
                disabled={trenutna === ukupno}
                className="w-9 h-9 rounded-md border border-slate-700 text-slate-400 hover:border-slate-500 disabled:opacity-30 transition-colors"
            >
                ›
            </button>
        </div>
    );
}
