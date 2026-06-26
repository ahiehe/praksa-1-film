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

const router = createBrowserRouter([
    {
        path: '/',
        element: <Layout />, 
        children: [
            {
                path: ROUTES.HOME, 
                element: <FilmList />
            },
            {
                path: ROUTES.LOGIN,
                element: <Login />
            },
            {
                path: ROUTES.REGISTER,
                element: <Register />
            },
            {
                element: <ProtectedRoute />,
                children: [
                    {
                        path: ROUTES.DETAILS_PATTERN,
                        element: <FilmDetails />
                    },
                ]
            },
            {
                element: <AdminRoute />,
                children: [
                    {
                        path: ROUTES.EDIT_PATTERN,
                        element: <FilmEdit />
                    },
                    {
                        path: ROUTES.CREATE, 
                        element: <FilmCreate />
                    },
                ]
            }

        ]
    }
]);

export default function App() {
    return <RouterProvider router={router} />;
}