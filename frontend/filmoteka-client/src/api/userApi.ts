import { CreateUserDTO, UpdateUserDTO } from '../types/dto';
import { UserInfo } from '../types/user';
import api from './axiosInstance';

const CONTROLLER = '/user';

export const getUsers = () =>
    api.get<UserInfo[]>(CONTROLLER).then(res => res.data);

export const getUserById = (id: number) =>
    api.get<UserInfo>(`${CONTROLLER}/${id}`).then(res => res.data);

export const createUser = (dto: CreateUserDTO) =>
    api.post<{ id: number }>(`${CONTROLLER}/create`, dto).then(res => res.data);

export const updateUser = (id: number, dto: UpdateUserDTO) =>
    api.put(`${CONTROLLER}/${id}`, dto).then(res => res.data);

export const deleteUser = (id: number) =>
    api.delete(`${CONTROLLER}/${id}`).then(res => res.data);