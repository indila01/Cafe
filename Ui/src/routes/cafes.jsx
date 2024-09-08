import { createFileRoute } from '@tanstack/react-router'
import { CafesPage } from '../pages/CafePage'

export const Route = createFileRoute('/cafes')({
  
  component: () => {
    const queryParams = new URLSearchParams(location.search);
    const cafeLocation = queryParams.get('location');
    return  <CafesPage location={cafeLocation} />}
})