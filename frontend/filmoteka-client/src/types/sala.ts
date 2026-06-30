export type TipSale = 'Standard' | 'IMAX' | '_3D';

export interface Sala {
    id: number;
    naziv: string;
    kapacitet: number;
    tip: TipSale;
}