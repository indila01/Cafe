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
    { headerName: 'Name', field: 'name' },
    { headerName: 'Description', field: 'description' },
    { headerName: 'Location', field: 'location' },
  ];

  return (
    <div
      className="ag-theme-alpine" // applying the Data Grid theme
      style={{ height: 500 }} // the Data Grid will fill the size of the parent container
    >
      <AgGridReact rowData={cafes} columnDefs={columns} />
    </div>
  );
};