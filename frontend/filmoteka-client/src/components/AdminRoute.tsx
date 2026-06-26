import { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { isUserAdmin } from '../utils/storage';
import { ROUTES } from '../constants/routes';

export const AdminRoute: FC = () => {
    return isUserAdmin() ? <Outlet /> : <Navigate to={ROUTES.HOME} replace />;
};