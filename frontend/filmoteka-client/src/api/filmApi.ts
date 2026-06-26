// api/filmApi.ts
import api from './axiosInstance';
import { Film, Zanr, ReziserOption } from '../types/film';
import { CreateFilmDTO, FilmQueryDTO, PaginatedFilmsDTO } from '../types/dto';

const CONTROLLER = '/film';

export const getPaginatedFilms = (query: FilmQueryDTO) =>
    api.get<PaginatedFilmsDTO>(CONTROLLER, { params: query }).then(res => res.data);

export const getFilmById = (id: number) =>
    api.get<Film>(`${CONTROLLER}/${id}`).then(res => res.data);

export const createFilm = (dto: CreateFilmDTO) =>
    api.post<{id: number}>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const updateFilm = (id: number, dto: CreateFilmDTO) =>
    api.put(`${CONTROLLER}/update/${id}`, dto).then(res => res.data);

export const deleteFilm = (id: number) =>
    api.delete(`${CONTROLLER}/delete/${id}`).then(res => res.data);

export const getZanrovi = () =>
    api.get<Zanr[]>(`${CONTROLLER}/zanrovi`).then(res => res.data);

export const getReziseri = () =>
    api.get<ReziserOption[]>(`${CONTROLLER}/reziseri`).then(res => res.data);