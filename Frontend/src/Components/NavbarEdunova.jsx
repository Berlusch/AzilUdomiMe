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
    window.open(PRODUKCIJA + "/../../swagger/index.html", "_blank");
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

        {/* Ovdje stavimo flex na desnu stranu */}
        <Nav
          className="ml-auto" // Dodaj ml-auto za lijevo poravnanje svih linkova na desnu stranu
          style={{
            display: 'flex',           // Omogućava Flexbox
            justifyContent: 'flex-end', // Poravnava sve na desnu stranu
            width: '100%',              // Osigurava da zauzme cijeli prostor
          }}
        >
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

              <Nav.Link onClick={OpenSwaggerURL} style={{ display: 'block' }}>
                <strong>Swagger</strong>
              </Nav.Link>

              <Nav.Link onClick={logout}>Odjava</Nav.Link>
            </>
          ) : (
            <Nav.Link
              onClick={() => navigate(RouteNames.LOGIN)}
              style={{
                display: 'inline-block',
                color: '#4B0082',               // Boja teksta
                backgroundColor: '#f0e6ff',    // Boja pozadine
                padding: '10px 20px',          // Padding za gumb
                borderRadius: '5px',           // Za zaobljene rubove
                textAlign: 'center', 
                marginRight: '10px',          // Poravnanje teksta
              }}
            >
              <strong>Prijava</strong>
            </Nav.Link>
          )}
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  );
}

