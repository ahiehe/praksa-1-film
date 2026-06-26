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