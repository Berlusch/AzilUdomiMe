import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { PRODUKCIJA, RouteNames } from '../constants';



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
                <Nav className="d-flex w-100 justify-content-between"style={{ paddingBottom: '30px' }} >
    <NavDropdown title={<span className="dropdown-title"><img src="/izbornik.png" alt="Izbornik" /></span>} id="basic-nav-dropdown">
        <NavDropdown.Item onClick={() => navigate(RouteNames.PAS_PREGLED)}>
            Psi
        </NavDropdown.Item>
        <NavDropdown.Item onClick={() => navigate(RouteNames.UDOMITELJ_PREGLED)}>
            Udomitelji
        </NavDropdown.Item>
        <NavDropdown.Item onClick={() => navigate(RouteNames.UPIT_PREGLED)}>
            Upiti
        </NavDropdown.Item>
        <NavDropdown.Item onClick={() => navigate(RouteNames.STATUS_PREGLED)}>
            Statusi
        </NavDropdown.Item>
        <NavDropdown.Item onClick={() => navigate(RouteNames.ERA_DIJAGRAM)}>
            ERA
        </NavDropdown.Item>
    </NavDropdown>
    

    <Nav.Link href={PRODUKCIJA + '/swagger'} target='_blank'
    style={{ display: 'inline-block' }}><strong>Swagger</strong></Nav.Link>            
    
</Nav>
                
                </Navbar.Collapse>
            </Container>
        </Navbar>
        </>
    )
}