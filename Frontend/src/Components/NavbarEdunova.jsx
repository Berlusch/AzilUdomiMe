import "bootstrap/dist/css/bootstrap.min.css"
import './App.css'
import { Container } from "react-bootstrap"
import { useNavigate } from "react-router-dom"

export default NavbarEdunova() {

  const navigate =useNavigate();

    return (
      <Navbar expand="lg" className="bg-body-tertiary">
        <Container>
          <Navbar.Brand href="#home">Udomi me</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              
              <NavDropdown title="Programi" id="basic-nav-dropdown">
                <NavDropdown.Item href="#action/3.1">Udomitelji</NavDropdown.Item>
                </NavDropdown>
                
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    );
  }
  