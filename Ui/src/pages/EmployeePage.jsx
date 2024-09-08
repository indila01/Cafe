import {
    QueryClient,
    QueryClientProvider,
  } from '@tanstack/react-query'
//   import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Employee } from '../components/employe.jsx';


export const EmployeePage = () => {
    const queryClient = new QueryClient()

  return (
    <QueryClientProvider client={queryClient}>
          <Employee/>
    </QueryClientProvider>

  );
};
