import { TipSale } from "./sala";


export interface TerminInfo {
    id: number;
    nazivFilma: string;
    nazivSale: string;
    brojDostupnihMesta: number;
    pocetakProjekcije: string;
    krajProjekcije: string;
    tipSale: TipSale;
}

export interface TerminDetails {
    id: number;
    nazivFilma: string;
    opisFilma: string;
    godinaIzdanja: number;
    zanrNaziv: string;
    reziseri: string[];
    nazivSale: string;
    kapacitetSale: number;
    brojDostupnihMesta: number;
    pocetakProjekcije: string;
    krajProjekcije: string;
    vecRezervisano: boolean;
    tipSale: TipSale;
}