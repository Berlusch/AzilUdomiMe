import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { PRODUKCIJA, RouteNames } from '../constants';
import useAuth from '../hooks/useAuth';

export default function NavBarEdunova() {
  const navigate = useNavigate();
  const { logout, isLoggedIn } = useAuth();

  function OpenSwaggerURL() {
    window.open(PRODUKCIJA + "/swagger/index.html", "_blank");
  }

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Navbar.Brand href="/">Edunova APP [WP6]</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="me-auto">
          <Nav.Link onClick={() => navigate(RouteNames.HOME)}>
            <img src="/logo-removebg-preview.png" alt="Logo" className="logo-navbar" />
          </Nav.Link>

          {isLoggedIn ? (
            <>
              <Nav.Link onClick={() => navigate(RouteNames.NADZORNA_PLOCA)}>
                Nadzorna ploča
              </Nav.Link>

              <NavDropdown
                title={<span className="dropdown-title"><img src="/izbornik.png" alt="Izbornik" /></span>}
                id="basic-nav-dropdown"
              >
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

              <Nav.Link onClick={OpenSwaggerURL} style={{ display: 'inline-block' }}>
                <strong>Swagger</strong>
              </Nav.Link>

              <Nav.Link onClick={logout}>Odjava</Nav.Link>
            </>
          ) : (
            <Nav.Link onClick={() => navigate(RouteNames.LOGIN)}>Prijava</Nav.Link>
          )}
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  );
}
