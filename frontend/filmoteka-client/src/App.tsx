import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import {Layout} from './components/Layout';
import FilmList from './pages/FilmList';
import { FilmCreate } from './pages/FilmCreate';
import { ROUTES } from './constants/routes';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import { FilmDetails } from './pages/FIlmDetails';
import { FilmEdit } from './pages/FIlmEdit';
import { AdminRoute } from './components/AdminRoute';
import { ProtectedRoute } from './components/ProtectedRoute';
import { EmployeeRoute } from './components/EmployeeRoute';
import { SalaCreate } from './pages/SalaCreate';
import { TerminCreate } from './pages/TerminCreate';
import { TerminList } from './pages/TerminList';
import { TerminDetails } from './pages/TerminDetails';

const router = createBrowserRouter([
    {
        path: '/',
        element: <Layout />,
        children: [
            { path: ROUTES.HOME, element: <FilmList /> },
            { path: ROUTES.LOGIN, element: <Login /> },
            { path: ROUTES.REGISTER, element: <Register /> },
            { path: ROUTES.TERMINI, element: <TerminList /> },
            {
                element: <ProtectedRoute />,
                children: [
                    { path: ROUTES.DETAILS_PATTERN, element: <FilmDetails /> },
                    { path: ROUTES.TERMIN_DETAILS_PATTERN, element: <TerminDetails /> },
                ]
            },
            {
                element: <AdminRoute />,
                children: [
                    { path: ROUTES.EDIT_PATTERN, element: <FilmEdit /> },
                    { path: ROUTES.CREATE, element: <FilmCreate /> },
                    { path: ROUTES.SALA_CREATE, element: <SalaCreate /> },
                ]
            },
            {
                element: <EmployeeRoute />,
                children: [
                    { path: ROUTES.TERMIN_CREATE, element: <TerminCreate /> },
                ]
            }
        ]
    }
]);

export default function App() {
    return <RouterProvider router={router} />;
}