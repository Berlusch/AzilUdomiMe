import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import UpitService from "../../services/UpitService";
import { useEffect, useState } from "react";
import moment from "moment";

export default function UpitiBrisanje(){

    const navigate = useNavigate();
    const [upit,setUpit]= useState({});
    const routeParams= useParams();

    async function dohvatiUpite(){
        const odgovor=await UpitService.getBySifra(routeParams.sifra)
        setUpit(odgovor)
    }

    useEffect(()=>{
        dohvatiUpite();
    },[])

    async function obrisi (){
        const odgovor= await UpitService.obrisi(routeParams.sifra,upit);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.UPIT_PREGLED)

        
        
    }

    function OdradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        let podatci = new FormData(e.target);

        obrisi(
            {           
 
                pasIme: podatci.get('pasIme'),
                upitImePrezime: podatci.get('upitImePrezime'),
                datumUpita: moment.utc(podatci.get('datumUpita')),
                statusUpita: podatci.get('statusUpita'),
                napomene: podatci.get('napomene')
            }

        );           
                
        
    }
    
    return(
    <>
    <h2 className="naslov">Brisanje upita</h2>
    <Form onSubmit={OdradiSubmit}>

    <Form.Group controlId="pasIme">
            <Form.Label>Ime psa</Form.Label>
            <Form.Control type="text" name="pasIme" required
            defaultValue={upit.pasIme}
            readonly/>
        </Form.Group>

        <Form.Group controlId="udomiteljImePrezime">
            <Form.Label>Ime i prezime udomitelja</Form.Label>
            <Form.Control type="text" name="udomiteljImePrezime" required
            defaultValue={upit.udomiteljImePrezime}
            readonly/>
        </Form.Group>

        <Form.Group controlId="datumUpita">
            <Form.Label>Datum upita (napomena: datum nije moguće promijeniti!)</Form.Label>
            <Form.Control type="date" name="datumUpita" required
            defaultValue={upit.datumUpita}
            readonly/>
            
        </Form.Group>

        <Form.Group controlId="datumUpita">
                <Form.Label>Datum upita</Form.Label>
                <Form.Control type="date" name="datumUpita" 
                readonly/>
            </Form.Group>

        <Form.Group controlId="statusUpita">
            <Form.Label>Status upita</Form.Label>
            <Form.Control type="text" name="statusUpita" required
            defaultValue={upit.statusUpita}
            readonly/>
        </Form.Group>

        <Form.Group controlId="napomene">
            <Form.Label>Napomene</Form.Label>
            <Form.Control type="text" name="napomene" required
            defaultValue={upit.napomene}
            readonly/>
        </Form.Group>
        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.Upit_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#9c989a' }}
                >Odustani <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" class="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg></Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko"
                style={{ backgroundColor: '#7d3d9b' }}
                >Obriši upit <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" class="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
                <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
              </svg>
                </Button>
            </Col>

        </Row>


    </Form>





    </>
    )
}