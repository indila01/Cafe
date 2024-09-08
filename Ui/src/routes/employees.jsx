import { createFileRoute } from '@tanstack/react-router'
import { EmployeePage } from '../pages/EmployeePage'
// import { useLocation } from 'react-router-dom';


export const Route = createFileRoute('/employees')({
    component: () => {
        // const location = useLocation();

     const queryParams = new URLSearchParams(location.search);
    const cafe = queryParams.get('cafe');
    return  <EmployeePage cafe={cafe} />}
  });