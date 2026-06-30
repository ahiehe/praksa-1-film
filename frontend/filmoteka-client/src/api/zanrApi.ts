import api from './axiosInstance';
import { Zanr } from '../types/film';
import { CreateZanrDTO } from '../types/dto';

const CONTROLLER = '/zanr';

export const getZanrovi = () =>
    api.get<Zanr[]>(CONTROLLER).then(res => res.data);

export const createZanr = (dto: CreateZanrDTO) =>
    api.post<{ id: number }>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const updateZanr = (id: number, dto: CreateZanrDTO) =>
    api.put(`${CONTROLLER}/update/${id}`, dto).then(res => res.data);

export const deleteZanr = (id: number) =>
    api.delete(`${CONTROLLER}/delete/${id}`).then(res => res.data);