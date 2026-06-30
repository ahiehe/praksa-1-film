export interface Zanr {
    id: number;
    naziv: string;
}

export interface Reziser {
    id: number;
    ime: string;
    prezime: string;
    datumRodjenja: string;
}

export interface ReziserOption {
    id: number;
    ime: string;
    prezime: string;
}

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