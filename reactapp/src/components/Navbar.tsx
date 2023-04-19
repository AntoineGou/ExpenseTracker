import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Link from '@mui/material/Link';

const Navbar: React.FC = () => {
    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" component="div">
                    <Link component={RouterLink} to="/" color="inherit" underline="none">
                        Home
                    </Link>
                </Typography>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;
