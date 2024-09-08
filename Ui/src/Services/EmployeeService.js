// import { loadConfig } from './ConfigService';

export const getEmployees = async ({cafe}) => {
    // const config = await loadConfig();
    let url = 'http://localhost:5001/Employees';
    if (cafe.cafe !== null) {
        url += `?cafe=${cafe.cafe}`;
    }
    const response = await fetch(url);
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
  };
  
  export const createEmployee = async (employee) => {
    const response = await fetch('http://localhost:5001/Employees', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(employee)
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};

export const updateEmployee = async (id, employee) => {
    const response = await fetch(`http://localhost:5001/Employees?id=${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(employee)
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};

export const deleteEmployee = async (id) => {
    const response = await fetch(`http://localhost:5001/Employees?employeeId=${id}`, {
        method: 'DELETE'
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};