import { useEffect, useState } from 'react';
import { fetchCafes } from '../Services/CafeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

export const Cafe = () => {
  const [cafes, setCafes] = useState([]);
  const { data, isPending, error } = useQuery({
    queryKey: ['cafes'],
    queryFn: fetchCafes,
  });

  useEffect(() => {
    if (data) {
      setCafes(data);
    }
  }, [data]);

  if (isPending) return <div>Loading...</div>;
  if (error) return <div>Error loading cafes</div>;

  const columns = [

    //filter with location
    //click on employees and show employees with cafe
    //add new cafe button
    { headerName: 'Name', field: 'name' },
    { headerName: 'Description', field: 'description' },
    { headerName: 'Location', field: 'location' },
    { headerName: 'Employees',  },
    { headerName: 'Edit/Delete' },
  ];

  return (
    <div
    className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
      <AgGridReact rowData={cafes} columnDefs={columns} />
    </div>
  );
};