import { useEffect, useState } from 'react';
import { fetchEmployees } from '../Services/EmployeeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';


export const Employee = () => {
    const [employees, setEmployees] = useState([]);
    const { data, isPending, error } = useQuery({
      queryKey: ['employees'],
      queryFn: fetchEmployees,
    });
  
    useEffect(() => {
      if (data) {
        setEmployees(data);
      }
    }, [data]);
  
    if (isPending) return <div>Loading...</div>;
    if (error) return <div>Error loading employees</div>;
  
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
        <AgGridReact rowData={employees} columnDefs={columns} />
      </div>
    );
  };