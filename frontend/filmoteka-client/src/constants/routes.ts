export const ROUTES = {
    HOME: '/',
    LOGIN: '/login',
    REGISTER: '/register',
    ADMIN_PANEL: '/admin',
    CREATE: '/create',
    EDIT: (id: number | string) => `/edit/${id}`,
    EDIT_PATTERN: `edit/:id`,
    DETAILS: (id: number | string) => `/details/${id}`,
    DETAILS_PATTERN: `/details/:id`,


    TERMINI: '/termini',
    TERMIN_DETAILS: (id: number | string) => `/termini/${id}`,
    TERMIN_DETAILS_PATTERN: '/termini/:id',
    TERMIN_CREATE: '/termini/create',

    ZANR_LIST: '/zanrovi',
    REZISER_LIST: '/reziseri',

    SALA_LIST: "/sale",
    SALA_CREATE: '/sale/create',

    USER_LIST: "/user",
    USER_EDIT: (id: number | string) => `/user/edit/${id}`,
    USER_EDIT_PATTERN: `/user/edit/:id`,

} as const;