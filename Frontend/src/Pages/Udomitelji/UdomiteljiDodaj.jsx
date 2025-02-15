import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import UdomiteljService from "../../services/UdomiteljService";
import moment from "moment"

export default function UdomiteljiDodaj(){

    const navigate = useNavigate();     

    async function dodaj(udomitelj){
        const odgovor= await UdomiteljService.dodaj(udomitelj);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.UDOMITELJ_PREGLED)
        
        

    }

    function odradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        let podatci = new FormData(e.target);

        dodaj(
            {            
 
                ime: podatci.get('ime'),
                prezime: podatci.get('prezime'),
                adresa: podatci.get('adresa'),
                telefon: podatci.get('telefon'),
                email: podatci.get('email')
            }
             
        
        );
    }

    
    return(
    <>
    <h2 className="naslov">Dodavanje udomitelja</h2>
    <Form onSubmit={odradiSubmit}>

        <Form.Group controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" required/>
        </Form.Group>

        <Form.Group controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" required/>
        </Form.Group>

        <Form.Group controlId="adresa">
            <Form.Label>Adresa</Form.Label>
            <Form.Control type="text" name="adresa" required/>
        </Form.Group>

        <Form.Group controlId="telefon">
            <Form.Label>Telefon</Form.Label>
            <Form.Control type="number" name="telefon" required/>
        </Form.Group>

        <Form.Group controlId="email">
            <Form.Label>Email</Form.Label>
            <Form.Control type="text" name="email" required/>
        </Form.Group>

        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.UDOMITELJ_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#E0B0FF' }}
                >Odustani</Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko" style={{ backgroundColor: '#7d3d9b' }}>
                    Dodaj udomitelja
                </Button>
            </Col>

        </Row>


    </Form>


    
    </>
    )

}
