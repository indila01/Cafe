import {
    QueryClient,
    QueryClientProvider,
  } from '@tanstack/react-query'
//   import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Cafe } from '../components/cafe.jsx';


export const CafesPage = (location) => {
    const queryClient = new QueryClient()

  return (
    <QueryClientProvider client={queryClient}>
          <Cafe location={location}/>
    </QueryClientProvider>

  );
};
