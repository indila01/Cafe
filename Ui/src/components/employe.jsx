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
      { headerName: 'Nameasd', field: 'name' },
      { headerName: 'Email', field: 'email' },
      { headerName: 'Phone Number', field: 'phoneNumber' },
      { headerName: 'Cafe', field: 'cafe' },
      { headerName: 'Days Worked', field: 'daysWorked' },
    ];
  
    return (
      <div
      className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
        <AgGridReact rowData={employees} columnDefs={columns} />
      </div>
    );
  };