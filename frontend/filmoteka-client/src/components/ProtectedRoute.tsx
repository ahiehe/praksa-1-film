import { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { isLoggedIn } from '../utils/storage';
import { ROUTES } from '../constants/routes';

export const ProtectedRoute: FC = () => {
    return isLoggedIn() ? <Outlet /> : <Navigate to={ROUTES.LOGIN} replace />;
};