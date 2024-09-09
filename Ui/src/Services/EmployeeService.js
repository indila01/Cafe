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
        body: JSON.stringify({
            "id": employee.id,
            "name": employee.name,
            "email": employee.email,
            "phoneNumber": employee.phoneNumber,
            "cafeId": employee.cafeId,
            "gender": employee.gender
        })
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};

export const updateEmployee = async (employee) => {
    var body = null
    if(employee.cafeId !== null && employee.cafeId !== ''){
        body = {  "id": employee.id,
            "name": employee.name,
            "email": employee.email,
            "phoneNumber": employee.phoneNumber,
            "cafeId": employee.cafeId,
            "gender": employee.gender}
    }
    else{
      body = {  "id": employee.id,
            "name": employee.name,
            "email": employee.email,
            "phoneNumber": employee.phoneNumber,
            "gender": employee.gender}
    }
    const response = await fetch(`http://localhost:5001/Employees?id=${employee.id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
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