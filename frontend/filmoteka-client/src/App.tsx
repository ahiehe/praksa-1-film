import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import {Layout} from './components/Layout';
import FilmList from './pages/FilmList';
import { FilmCreate } from './pages/FilmCreate';
import { ROUTES } from './constants/routes';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import { FilmDetails } from './pages/FIlmDetails';
import { FilmEdit } from './pages/FIlmEdit';

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
                path: ROUTES.CREATE, 
                element: <FilmCreate />
            },
            {
                path: ROUTES.DETAILS_PATTERN,
                element: <FilmDetails />
            },
            {
                path: ROUTES.EDIT_PATTERN,
                element: <FilmEdit />
            }

        ]
    }
]);

export default function App() {
    return <RouterProvider router={router} />;
}