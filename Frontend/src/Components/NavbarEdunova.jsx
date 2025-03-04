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
    window.open(`${PRODUKCIJA}/../../swagger/index.html`, "_blank");
  }

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="me-auto">
          <Nav.Link onClick={() => navigate(RouteNames.HOME)}>
            <img src="/logo-removebg-preview.png" alt="Logo" className="logo-navbar" />
          </Nav.Link>
        </Nav>        

        {isLoggedIn && (
          <Nav className="me-auto">
            <Nav.Link onClick={() => navigate(RouteNames.NADZORNA_PLOCA)}>
              <h4>Nadzorna ploča</h4>
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
          </Nav>
        )}
      </Navbar.Collapse>

      <Nav
        className="ml-auto"
        style={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'flex-end',
          gap: '10px',
        }}
      >
        {isLoggedIn ? (
          <>
            <Nav.Link
              onClick={logout}
              style={{
                display: 'inline-block',
                color: 'white',
                backgroundColor: '#7f7e80',
                padding: '10px 20px',
                borderRadius: '5px',
                textAlign: 'center',
                height: '45px',
                lineHeight: 'normal',
              }}
            >
              <strong>Odjava</strong>
            </Nav.Link>

            <Nav.Link onClick={OpenSwaggerURL}>
              <strong>Swagger</strong>
            </Nav.Link>
          </>
        ) : (
          <Nav.Link
            onClick={() => navigate(RouteNames.LOGIN)}
            style={{
              display: 'block',
              color: '#4B0082',
              backgroundColor: '#f0e6ff',
              padding: '10px 20px',
              borderRadius: '5px',
              textAlign: 'center',
              marginRight: '10px',
            }}
          >
            <strong>Prijava</strong>
          </Nav.Link>
        )}
      </Nav>
    </Navbar>
  );
}
