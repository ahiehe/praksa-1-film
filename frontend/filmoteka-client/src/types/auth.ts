export interface Register {
    username: string;
    email: string;
    password: string;
}

export interface Login {
    usernameOrEmail: string;
    password: string;
}

export interface AuthResponse {
    token: string;
    username: string;
}
