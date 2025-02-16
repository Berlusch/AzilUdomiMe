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
    },[routeParams.sifra])

    async function obrisi (){
        const odgovor= await UdomiteljService.obrisi(routeParams.sifra);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.UDOMITELJ_BRISANJE)

        
        
    }

    function OdradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        let podatci = new FormData(e.target);

        obrisi();           
                
        
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
                >Odustani</Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko"
                style={{ backgroundColor: '#7d3d9b' }}
                >Obrisi udomitelja
                </Button>
            </Col>

        </Row>


    </Form>





    </>
    )
}