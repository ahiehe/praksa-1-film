import { Film } from "./film";

export interface CreateFilmDTO {
    naziv: string;
    godinaIzdanja: number;
    zanrId: number;
    opis: string;
    izabraniReziseri: number[];
    pocetakPrikazivanja: string | null;
    krajPrikazivanja: string | null;
}

export interface PaginatedFilmsDTO {
    filmovi: Film[];
    trenutnaStranica: number;
    ukupnoStranica: number;
}


export interface FilmQueryDTO {
    page: number;
    search?: string;
    uBioskopima?: boolean; 
    godinaOd?: number; 
    godinaDo?: number;
    zanrId?: number;
}