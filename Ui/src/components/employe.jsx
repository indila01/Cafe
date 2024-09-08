import { useEffect, useState } from 'react';
import { getEmployees } from '../Services/EmployeeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';


export const Employee = (cafe) => {
    const [employees, setEmployees] = useState([]);
    const { data, isPending, error } = useQuery({
      queryKey: ['employees'],
      queryFn: ()=>getEmployees(cafe),
    });
  
    useEffect(() => {
      if (data) {
        setEmployees(data);
      }
    }, [data]);
  
    if (isPending) return <div>Loading...</div>;
    if (error) return <div>Error loading employees</div>;
  
    const columns = [
      { headerName: 'Namea', field: 'name' },
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