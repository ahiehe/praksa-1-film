export const ROUTES = {
    HOME: '/',
    CREATE: '/create',
    EDIT: (id: number | string) => `/edit/${id}`,
    EDIT_PATTERN: `edit/:id`,
    DETAILS: (id: number | string) => `/details/${id}`,
    DETAILS_PATTERN: `/details/:id`,
    LOGIN: '/login',
    REGISTER: '/register',

    TERMINI: '/termini',
    TERMIN_DETAILS: (id: number | string) => `/termini/${id}`,
    TERMIN_DETAILS_PATTERN: '/termini/:id',
    TERMIN_CREATE: '/termini/create',

    SALA_CREATE: '/sale/create',
} as const;