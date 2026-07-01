import { Reziser } from "./reziser";
import { Zanr } from "./zanr";

export interface Film {
    id: number;
    naziv: string;
    opis: string;
    godinaIzdanja: number;
    zanrId?: number;
    zanr: Zanr;
    reziseri: Reziser[];
    pocetakPrikazivanja?: string;
    krajPrikazivanja?: string;
}

export interface FilmOption {
    id: number;
    naziv: string;
}