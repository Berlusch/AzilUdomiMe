import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import UdomiteljService from "../../services/UdomiteljService";
import { useEffect, useState } from "react";

export default function UdomiteljiBrisanje(){

    const navigate = useNavigate();
    const [udomitelj,setUdomitelj]= useState({});
    const routeParams= useParams();

    async function dohvatiUdomitelje(){
        const odgovor=await UdomiteljService.getBySifra(routeParams.sifra)
        setUdomitelj(odgovor)
    }

    useEffect(()=>{
        dohvatiUdomitelje();
    },[])

    async function obrisi (){
        const odgovor= await UdomiteljService.obrisi(routeParams.sifra,udomitelj);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.UDOMITELJ_PREGLED)

        
        
    }

    function OdradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        let podatci = new FormData(e.target);

        obrisi(
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
    <h2 className="naslov">Brisanje udomitelja</h2>
    <Form onSubmit={OdradiSubmit}>

        <Form.Group controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" required
            defaultValue={udomitelj.ime}
            readOnly/>
        </Form.Group>

        <Form.Group controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" required
            defaultValue={udomitelj.prezime}
            readOnly/>
        </Form.Group>

        <Form.Group controlId="adresa">
            <Form.Label>Adresa</Form.Label>
            <Form.Control type="text" name="adresa" required
            defaultValue={udomitelj.adresa}
            readOnly/>
        </Form.Group>

        <Form.Group controlId="telefon">
            <Form.Label>Telefon</Form.Label>
            <Form.Control type="text" name="telefon" required
            defaultValue={udomitelj.telefon}
            readOnly/>
        </Form.Group>

        <Form.Group controlId="email">
            <Form.Label>Email</Form.Label>
            <Form.Control type="text" name="email" required
            defaultValue={udomitelj.email}
            readOnly/>
        </Form.Group>

        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.UDOMITELJ_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#9c989a' }}
                >Odustani <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" class="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg></Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko"
                style={{ backgroundColor: '#7d3d9b' }}
                >Obriši udomitelja <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" class="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
                <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
              </svg>
                </Button>
            </Col>

        </Row>


    </Form>





    </>
    )
}