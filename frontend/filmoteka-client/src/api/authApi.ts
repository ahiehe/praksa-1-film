import api from './axiosInstance';
import { AuthResponse, Login, Register } from '../types/auth';

const CONTROLLER = '/auth';

export const register = (dto: Register) =>
    api.post<AuthResponse>(`${CONTROLLER}/register`, dto).then(res => res.data);

export const login = (dto: Login) =>
    api.post<AuthResponse>(`${CONTROLLER}/login`, dto).then(res => res.data);