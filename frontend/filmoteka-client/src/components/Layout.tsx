import { FC, useState } from 'react';
import { Link, Outlet } from 'react-router-dom';
import { clearAuth, getUsername, isLoggedIn } from '../utils/storage';
import { ROUTES } from '../constants/routes';

export const Layout: FC = () => {
    const [isLogged, setIsLogged] = useState(isLoggedIn());

    return (
        <div className="min-h-screen bg-slate-900 text-white">
            <nav className="bg-slate-800 border-b border-slate-700 p-4">
                <div className="max-w-6xl mx-auto flex justify-between items-center">
                    <Link to={ROUTES.HOME} className="text-xl font-bold text-indigo-400 tracking-wider">
                        FILMOTEKA
                    </Link>
                    <div className="flex gap-4 items-center">
                        { isLogged ? (
                            <div className="flex gap-8 items-center">
                                <div className="flex items-center gap-2">
                                    <div className="w-7 h-7 rounded-full bg-indigo-900 flex items-center justify-center text-xs font-medium text-indigo-300">
                                        {getUsername()?.charAt(0).toUpperCase()}
                                    </div>
                                    <span className="text-slate-300">{getUsername()}</span>
                                </div>
                                <button 
                                    onClick={() => { clearAuth(); window.location.href = ROUTES.HOME; }}
                                    className="text-slate-300 hover:text-white transition-colors"
                                >
                                    Odjava
                                </button>
                            </div>
                        ) : (
                            <>
                                <Link to={ROUTES.LOGIN} className="text-slate-300 hover:text-white transition-colors">
                                    Prijava
                                </Link>
                                <Link to={ROUTES.REGISTER} className="border border-indigo-500 hover:bg-indigo-500 px-3 py-1.5 rounded-md text-sm font-medium transition-colors">
                                    Registracija
                                </Link>
                            </>
                        )}
                    </div>
                </div>
            </nav>

            <main className="max-w-6xl mx-auto p-4">
                <Outlet />
            </main>
        </div>
    );
}