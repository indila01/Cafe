import { useEffect, useState } from 'react';
import { getCafes, deleteCafe } from '../Services/CafeService.js';
import { useQuery } from '@tanstack/react-query';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { Button, Input, Modal } from "antd";
import { Link } from '@tanstack/react-router';

export const Cafe = (location) => {
  const [cafes, setCafes] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [searchInput, setSearchInput] = useState('');
  const { data, isPending, error, refetch } = useQuery({
    queryKey: ['cafes'],
    queryFn: ()=>getCafes(location),
  });

  useEffect(() => {
    if (data) {
      setCafes(data);
    }
  }, [data]);

  if (isPending) return <div>Loading...</div>;
  if (error) return <div>Error loading cafes</div>;

  const filteredCafes = cafes.filter(cafe =>
    cafe.location.toLowerCase().includes(searchQuery.toLowerCase())
  );

  const handleDelete = async (id) => {
    Modal.confirm({
      title: 'Are you sure you want to delete this cafe?',
      onOk: async () => {
        await deleteCafe(id);
        refetch();
      },
    });
  };

  const handleSearch = () => {
    setSearchQuery(searchInput);
  };

  const handleAddCafe = async () => {
    refetch();
  };

  const columns = [
    { headerName: 'Name', field: 'name' },
    { headerName: 'Description', field: 'description' },
    { headerName: 'Location', field: 'location' },
    { 
      headerName: 'Employees', 
      cellRenderer:  (params) => (
        <Button>
          <Link to={`/employees?cafe=${params.data.name}`}>Employees</Link>
        </Button>
      )
    },
    { headerName: 'Action', 
      cellRenderer:  (params) => (
      <>
        <Button style={{ marginRight: '8px' }}>Edit</Button>
        <Button danger
          onClick={() => handleDelete(params.data.id)}>
            Delete
        </Button>
      </>)} 
  ];

  return (
    <div>
      <div style={{ display: 'flex', marginBottom: 20 }}>
        <Input 
          placeholder="Search cafes" 
          value={searchInput} 
          onChange={(e) => setSearchInput(e.target.value)} 
          style={{ width: 200, marginRight: 10 }}
        />
        <Button onClick={handleSearch}>Search</Button>
        <Button onClick={handleAddCafe} style={{ marginLeft: 'auto' }}>Add Cafe</Button>
      </div>
      <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
        <AgGridReact rowData={filteredCafes} columnDefs={columns} />
      </div>
    </div>
  );
};