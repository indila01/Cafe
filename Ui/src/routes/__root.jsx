import { Outlet, createRootRoute } from '@tanstack/react-router'
import HamburgerMenu from '../components/hamburgerMenu'

export const Route = createRootRoute({
  component: () => 
  <>
  <div className="hamburger-menu-container">
    <HamburgerMenu/>
  </div>
    <Outlet />
  </> 
})
