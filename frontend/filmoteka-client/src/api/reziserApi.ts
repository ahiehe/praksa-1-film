import api from './axiosInstance';
import { ReziserOption } from '../types/film';
import { CreateReziserDTO } from '../types/dto';

const CONTROLLER = '/reziser';

export const getReziseri = () =>
    api.get<ReziserOption[]>(CONTROLLER).then(res => res.data);

export const createReziser = (dto: CreateReziserDTO) =>
    api.post<{ id: number }>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const updateReziser = (id: number, dto: CreateReziserDTO) =>
    api.put(`${CONTROLLER}/update/${id}`, dto).then(res => res.data);

export const deleteReziser = (id: number) =>
    api.delete(`${CONTROLLER}/delete/${id}`).then(res => res.data);