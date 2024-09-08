// import { loadConfig } from './ConfigService';


export const getCafes = async () => {
//   const config = await loadConfig();
  const response = await fetch('http://localhost:5001/Cafes');
  if (!response.ok) throw new Error('Network response was not ok');
  return response.json();
};

export const createCafes = async (data) => {
    const response = await fetch('http://localhost:5001/Cafes', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};

export const updateCafe = async (id, data) => {
    const response = await fetch(`http://localhost:5001/Cafes/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};

export const deleteCafe = async (id) => {
    const response = await fetch(`http://localhost:5001/Cafes?cafeid=${id}`, {
        method: 'DELETE'
    });
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};