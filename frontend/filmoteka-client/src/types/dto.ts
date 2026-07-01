import { Film } from "./film";
import { TipSale } from "./sala";
import { Role } from "./user";

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


export interface RezervacijaTerminaDTO {
    terminId: number;
}

export interface CreateTerminDTO {
    salaId: number;
    filmId: number;
    pocetakProjekcije: string;
    krajProjekcije: string;
}

export interface CreateSalaDTO {
    naziv: string;
    kapacitet: number;
    tip: TipSale;
}

export interface CreateReziserDTO {
    ime: string;
    prezime: string;
    datumRodjenja?: string;
}

export interface CreateZanrDTO {
    naziv: string;
}

export interface CreateUserDTO {
    username: string;
    email: string;
    password: string;
    role: Role;
}

export interface UpdateUserDTO {
    username?: string;
    email?: string;
    password?: string;
    role?: Role;
}