export const getCafes = async ({location}) => {
//   const config = await loadConfig();
    let url ='http://localhost:5001/Cafes';
    if (location.location !== null) {
        url += `?location=${location.location}`;
    }
    const response = await fetch(url);

    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
};

export const createCafe = async (data) => {
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

export const updateCafe = async (data) => {
    const response = await fetch(`http://localhost:5001/Cafes?id=${data.id}`, {
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