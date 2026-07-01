export type Role = 'Admin' | 'Employee' | 'User';

export interface UserInfo {
    id: number;
    username: string;
    email: string;
    role: Role;
}

