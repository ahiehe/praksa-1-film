import { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { isUserAdmin, isUserEmployee } from '../utils/storage';
import { ROUTES } from '../constants/routes';

export const EmployeeRoute: FC = () => {
    return isUserEmployee() || isUserAdmin() ? <Outlet /> : <Navigate to={ROUTES.HOME} replace />;
};