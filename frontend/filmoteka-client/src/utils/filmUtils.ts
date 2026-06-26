import { Film } from '../types/film';

export const isUBioskopima = (film: Film): boolean => {
    if (!film.pocetakPrikazivanja || !film.krajPrikazivanja) return false;
    const now = new Date();
    return new Date(film.pocetakPrikazivanja) <= now && new Date(film.krajPrikazivanja) >= now;
};