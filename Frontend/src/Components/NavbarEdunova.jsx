import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { RouteNames } from '../constants';



export default function NavBarEdunova(){

    const navigate = useNavigate(); // ; u pravilu i ne treba


    return(
        <>
        <Navbar expand="lg" className="bg-body-tertiary">
            <Container>
                <Navbar.Brand 
                className='ruka'
                onClick={()=>navigate(RouteNames.HOME)}
                ><img src="/logo-removebg-preview.png" alt="Logo" className="logo-navbar" /></Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="d-flex w-100 justify-content-between"style={{ paddingBottom: '20px' }} >
    <NavDropdown title={<span className="dropdown-title">Udomitelji</span>} id="basic-nav-dropdown">
        <NavDropdown.Item onClick={() => navigate(RouteNames.UDOMITELJ_PREGLED)}>
            Udomitelji
        </NavDropdown.Item>
    </NavDropdown>

    <Nav.Link
        href="https://blusch-001-site1.qtempurl.com/swagger"
        target="_blank"
        style={{ backgroundColor: '#00ff00' }}
        className="ms-auto"
    >
        Swagger
    </Nav.Link>
</Nav>
                
                </Navbar.Collapse>
            </Container>
        </Navbar>
        </>
    )
}