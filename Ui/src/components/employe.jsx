import { useEffect, useState } from 'react';
import { deleteEmployee, getEmployees } from '../Services/EmployeeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button,Modal } from "antd";


export const Employee = (cafe) => {
    const [employees, setEmployees] = useState([]);
    const { data, isPending, error, refetch } = useQuery({
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
      
        { headerName: 'Action', 
            cellRenderer:  (params) => (
            <>
              <Button style={{ marginRight: '8px' }}>Edit</Button>
              <Button danger
                 onClick={() => handleDelete(params.data.id)}>
             Delete
          </Button>
            </>)},
    ];

    const handleDelete = async (id) => {
        Modal.confirm({
          title: 'Are you sure you want to delete this Employee?',
          onOk: async () => {
            await deleteEmployee(id);
            refetch();
          },
        });
      };

    const handleAddEmployee = async () => {
        // const newCafe = { name: 'New Cafe', description: 'Description', location: 'Location' };
        // await addCafe(newCafe);
        refetch();
      };
    return (
        <div>
           < div style={{ display: 'flex', marginBottom: 20 }}>
             <Button onClick={handleAddEmployee} style={{ marginLeft: 'auto' }}>Add Employee</Button>
           </div>
            <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
                <AgGridReact rowData={employees} columnDefs={columns} />
             </div>

        </div>
     
    );
  };