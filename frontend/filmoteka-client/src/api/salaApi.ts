import api from './axiosInstance';
import { CreateSalaDTO } from '../types/dto';
import { Sala } from '../types/sala';

const CONTROLLER = '/sala';

export const getSale = () =>
    api.get<Sala[]>(CONTROLLER).then(res => res.data);

export const getSalaById = (id: number) =>
    api.get<Sala>(`${CONTROLLER}/${id}`).then(res => res.data);

export const createSala = (dto: CreateSalaDTO) =>
    api.post<{ id: number }>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const deleteSala = (id: number) =>
    api.delete(`${CONTROLLER}/${id}`).then(res => res.data);