import api from './axiosInstance';
import { CreateReziserDTO } from '../types/dto';
import { Reziser, ReziserOption } from '../types/reziser';

const CONTROLLER = '/reziser';

export const getReziseri = () =>
    api.get<Reziser[]>(CONTROLLER).then(res => res.data);

export const getReziseriOptions = () =>
    api.get<ReziserOption[]>(`${CONTROLLER}/options`).then(res => res.data);

export const createReziser = (dto: CreateReziserDTO) =>
    api.post<{ id: number }>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const updateReziser = (id: number, dto: CreateReziserDTO) =>
    api.put(`${CONTROLLER}/update/${id}`, dto).then(res => res.data);

export const deleteReziser = (id: number) =>
    api.delete(`${CONTROLLER}/delete/${id}`).then(res => res.data);