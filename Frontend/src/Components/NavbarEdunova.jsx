import React, { useRef, useState, useEffect } from 'react';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { PRODUKCIJA, RouteNames } from '../constants';
import useAuth from '../hooks/useAuth';

export default function NavBarEdunova() {
  const navigate = useNavigate();
  const { logout, isLoggedIn } = useAuth();
  const loginRef = useRef(null);
  const [showBubble, setShowBubble] = useState(false);
  const [animateBubble, setAnimateBubble] = useState(false); // kontrola animacije

  useEffect(() => {
    if (!isLoggedIn) {
      setShowBubble(true);
      // mali timeout da CSS tranzicija proradi
      const timer = setTimeout(() => setAnimateBubble(true), 50);
      return () => clearTimeout(timer);
    } else {
      setAnimateBubble(false);
      setShowBubble(false);
    }
  }, [isLoggedIn]);

  function OpenSwaggerURL() {
    window.open(`${PRODUKCIJA}/../../swagger/index.html`, "_blank");
  }

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="me-auto">
          <Nav.Link onClick={() => navigate(RouteNames.HOME)}>
            <img src="/azil-logo.png" alt="Logo" className="logo-navbar" />
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
              <NavDropdown.Item onClick={() => navigate(RouteNames.PAS_PREGLED)}>Psi</NavDropdown.Item>
              <NavDropdown.Item onClick={() => navigate(RouteNames.UDOMITELJ_PREGLED)}>Udomitelji</NavDropdown.Item>
              <NavDropdown.Item onClick={() => navigate(RouteNames.UPIT_PREGLED)}>Upiti</NavDropdown.Item>
              <NavDropdown.Item onClick={() => navigate(RouteNames.STATUS_PREGLED)}>Statusi</NavDropdown.Item>
              <NavDropdown.Item onClick={() => navigate(RouteNames.ERA_DIJAGRAM)}>ERA</NavDropdown.Item>
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
          position: 'relative',
        }}
      >
        {!isLoggedIn && (
          <div ref={loginRef} style={{ position: 'relative' }}>
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
                zIndex: 1,
              }}
            >
              <strong>Prijava</strong>
            </Nav.Link>

            {showBubble && (
              <div
                style={{
                  position: 'absolute',
                  top: '120%',
                  right: 0,
                  backgroundColor: '#f0e6ff',
                  color: '#4B0082',
                  padding: '8px 12px',
                  borderRadius: '8px',
                  boxShadow: '0 4px 8px rgba(0,0,0,0.2)',
                  whiteSpace: 'nowrap',
                  zIndex: 10,
                  fontSize: '0.75rem',
                  opacity: animateBubble ? 1 : 0,
                  transform: animateBubble ? 'translateY(0)' : 'translateY(-10px)',
                  transition: 'opacity 0.5s ease, transform 0.5s ease',
                }}
              >
                Prijavite se kako biste testirali CRUD funkcionalnosti aplikacije!
                <div
                  style={{
                    position: 'absolute',
                    top: '-8px',   
                    right: '10px',
                    width: 0,
                    height: 0,
                    borderLeft: '8px solid transparent',
                    borderRight: '8px solid transparent',
                    borderBottom: '8px solid #f0e6ff',
                  }}
                />
              </div>
            )}
          </div>
        )}

        {isLoggedIn && (
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
        )}
      </Nav>
    </Navbar>
  );
}