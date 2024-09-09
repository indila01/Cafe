import  { useState } from 'react';
import { Drawer, IconButton } from '@mui/material';
import { Link } from '@tanstack/react-router';
import { MenuOutlined } from '@ant-design/icons';
import { Menu } from 'antd';

const HamburgerMenu = () => {
  const [isOpen, setIsOpen] = useState(false);

  const toggleDrawer = (open) => (event) => {
    if (
      event.type === 'keydown' &&
      (event.key === 'Tab' || event.key === 'Shift')
    ) {
      return;
    }
    setIsOpen(open);
  };

  const menuItems = [
    {
      key: '1',
      label: <Link to="/cafes">Cafes</Link>,
    },
    {
      key: '2',
      label: <Link to="/employees">Employees</Link>,
    },
  ];

  return (
    <>
      <IconButton
        edge="start"
        color="inherit"
        aria-label="menu"
        onClick={toggleDrawer(true)}
      >
        <MenuOutlined />
      </IconButton>
      <Drawer anchor="left" open={isOpen} onClose={toggleDrawer(false)}>
        <div
          role="presentation"
          onClick={toggleDrawer(false)}
          onKeyDown={toggleDrawer(false)}
          style={{ width: 250 }}
        >
          <Menu
            mode="inline"
            items={menuItems}
          />
        </div>
      </Drawer>
    </>
  );
};

export default HamburgerMenu;