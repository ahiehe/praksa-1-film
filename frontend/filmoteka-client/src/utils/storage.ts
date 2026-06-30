const TOKEN_KEY= 'token';
const USERNAME_KEY = 'username';

export const saveAuth = (token: string, username: string) => {
    localStorage.setItem(TOKEN_KEY, token);
    localStorage.setItem(USERNAME_KEY, username);
};

export const getToken = () => localStorage.getItem(TOKEN_KEY);
export const getUsername = () => localStorage.getItem(USERNAME_KEY);

export const clearAuth = () => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USERNAME_KEY);
};

export const isLoggedIn = (): boolean => !!getToken();

export const parseToken = (token: string) => {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
};

export const getRoleFromToken = (): string | null => {
    const token = getToken();
    if (!token) return null;
    return parseToken(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
};

export const isUserAdmin = (): boolean => {
    return getRoleFromToken() === "Admin";
}

export const isUserEmployee = (): boolean => {
    return getRoleFromToken() === "Employee";
}

