export const ROUTES = {
    HOME: '/',
    CREATE: '/create',
    EDIT: (id: number | string) => `/edit/${id}`,
    EDIT_PATTERN: `edit/:id`,
    DETAILS: (id: number | string) => `/details/${id}`,
    DETAILS_PATTERN: `/details/:id`,
    LOGIN: '/login',
    REGISTER: '/register',
} as const;