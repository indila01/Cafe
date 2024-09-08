import { createRouter } from '@tanstack/react-router';


import CafesPage from './Pages/CafesPage';

const routes = [
  {
    path: '/',
    element: <CafesPage />,
  }
];

const router = createRouter({
  routes,
});

export default router;