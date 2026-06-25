import { Film } from "./film";

export interface CreateFilmDTO {
    naziv: string;
    godinaIzdanja: number;
    zanrId: number;
    opis: string;
    izabraniReziseri: number[];
}

export interface PaginatedFilmsDTO {
    filmovi: Film[];
    trenutnaStranica: number;
    ukupnoStranica: number;
}