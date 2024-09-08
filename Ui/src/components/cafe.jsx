import { useEffect, useState } from 'react';
import { getCafes, deleteCafe } from '../Services/CafeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button } from "antd";
import { Link } from '@tanstack/react-router';

export const Cafe = () => {
  const [cafes, setCafes] = useState([]);
  const { data, isPending, error } = useQuery({
    queryKey: ['cafes'],
    queryFn: getCafes,
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
    { 
      headerName: 'Employees', 
      cellRenderer:  (params) => (
        <Button>
          <Link to={`/employees?cafe=${params.data.name}`}>Employees</Link>
        </Button>
      )
    },
    { headerName: 'Delete', 
      cellRenderer:  (params) => (
        <Button danger
           onClick={() => deleteCafe(params.data.id)}>
           Delete
        </Button>
      ) },
  ];

  return (
    <div
    className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
      <AgGridReact rowData={cafes} columnDefs={columns} />
    </div>
  );
};