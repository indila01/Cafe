import { useEffect, useState } from 'react';
import { deleteEmployee, getEmployees } from '../Services/EmployeeService.js';
import { getCafes } from '../Services/CafeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button, Modal } from "antd";
import  EmployeeModal  from './modals/employeeModal.jsx';
export const Employee = (cafe) => {
  const [employees, setEmployees] = useState([]);
  const [cafes, setCafes] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);
  const [currentEmployee, setCurrentEmployee] = useState(null);

  const { data: employeeData, isLoading: isEmployeeLoading, error: employeeError, refetch: refetchEmployees } = useQuery({
    queryKey: ['employees'],
    queryFn: () => getEmployees(cafe),
  });

  const { data: cafeData, isLoading: isCafeLoading, error: cafeError, refetch: refetchCafes } = useQuery({
    queryKey: ['cafes'],
    queryFn: getCafes,
  });

  useEffect(() => {
    if (employeeData) {
      setEmployees(employeeData);
    }
  }, [employeeData]);

  useEffect(() => {
    if (cafeData) {
      setCafes(cafeData);
    }
  }, [cafeData]);

  if (isEmployeeLoading || isCafeLoading) return <div>Loading...</div>;
  if (employeeError || cafeError) return <div>Error loading data</div>;

  const columns = [
    { headerName: 'Name', field: 'name' },
    { headerName: 'Email', field: 'email' },
    { headerName: 'Phone Number', field: 'phoneNumber' },
    { headerName: 'Gender', field: 'gender' },
    { headerName: 'Cafe', field: 'cafe' },
    {
      headerName: 'Action',
      cellRenderer: (params) => (
        <>
          <Button
            style={{ marginRight: '8px' }}
            onClick={() => handleEditEmployee(params.data)}
          >
            Edit
          </Button>
          <Button
            danger
            onClick={() => handleDelete(params.data.id)}
          >
            Delete
          </Button>
        </>
      ),
    },
  ];

  const handleDelete = async (id) => {
    Modal.confirm({
      title: 'Are you sure you want to delete this Employee?',
      onOk: async () => {
        await deleteEmployee(id);
        refetchEmployees();
      },
    });
  };

  const handleAddEmployee = () => {
    setIsEditMode(false);
    setCurrentEmployee(null);
    setIsModalVisible(true);
  };

  const handleEditEmployee = (employee) => {
    setIsEditMode(true);
    setCurrentEmployee(employee);
    setIsModalVisible(true);
  };

  const handleModalCancel = () => {
    setIsModalVisible(false);
  };

  const handleModalSubmit = async (employee) => {
    // Add or update employee logic here
    // Example: await addOrUpdateEmployee(employee);
    refetchEmployees();
    setIsModalVisible(false);
  };

  return (
    <div>
      <div style={{ display: 'flex', marginBottom: 20 }}>
        <Button onClick={handleAddEmployee} style={{ marginLeft: 'auto' }}>Add Employee</Button>
      </div>
      <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
        <AgGridReact rowData={employees} columnDefs={columns} />
      </div>
      {isModalVisible && (
        <EmployeeModal
          isVisible={isModalVisible}
          isEditMode={isEditMode}
          onCancel={handleModalCancel}
          onSubmit={handleModalSubmit}
          initialValues={currentEmployee}
          cafes={cafes}
        />
      )}
    </div>
  );
};