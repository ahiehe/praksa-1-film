import api from './axiosInstance';
import { TerminInfo, TerminDetails } from '../types/termin';
import { CreateTerminDTO, RezervacijaTerminaDTO } from '../types/dto';

const CONTROLLER = '/termin';

export const getActiveTermini = () =>
    api.get<TerminInfo[]>(CONTROLLER).then(res => res.data);

export const getTerminById = (id: number) =>
    api.get<TerminDetails>(`${CONTROLLER}/${id}`).then(res => res.data);

export const reserveTermin = (dto: RezervacijaTerminaDTO) =>
    api.post(`${CONTROLLER}/reserve`, dto).then(res => res.data);

export const createTermin = (dto: CreateTerminDTO) =>
    api.post<{ id: number }>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const updateTermin = (id: number, dto: CreateTerminDTO) =>
    api.put(`${CONTROLLER}/${id}`, dto).then(res => res.data);

export const deleteTermin = (id: number) =>
    api.delete(`${CONTROLLER}/${id}`).then(res => res.data);