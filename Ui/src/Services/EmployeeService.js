// import { loadConfig } from './ConfigService';

export const fetchEmployees = async () => {
    // const config = await loadConfig();
    const response = await fetch('http://localhost:5001/Employees');
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
  };
  