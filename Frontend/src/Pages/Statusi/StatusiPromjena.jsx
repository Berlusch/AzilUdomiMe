import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import StatusService from "../../services/StatusService";
import { useEffect, useState } from "react";

export default function StatusiPromjena(){

    const navigate = useNavigate();
    const [status,setStatus]= useState({});
    const routeParams= useParams();

    async function dohvatiStatuse(){
        const odgovor=await StatusService.getBySifra(routeParams.sifra)
        setStatus(odgovor)
    }

    useEffect(()=>{
        dohvatiStatuse();
    },[])

    async function promijeni(status){
        const odgovor= await StatusService.promijeni(routeParams.sifra,status);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.STATUS_PREGLED)

        
        
    }

    function OdradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        let podatci = new FormData(e.target);

        promijeni(
            {           
 
                naziv: podatci.get('naziv'),
                
            }
                
        );
    }

    
    return(
    <>
    <h2 className="naslov">Promjena statusa</h2>
    <Form onSubmit={OdradiSubmit}>

        <Form.Group controlId="ime">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" required
            defaultValue={status.naziv}/>
        </Form.Group>

        
        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.STATUS_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#9c989a' }}
                >Odustani<svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" className="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg> </Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko"style={{ backgroundColor: '#7d3d9b' }}>
                    Promijeni status <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" className="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
  <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
</svg>
                </Button>
            </Col>

        </Row>


    </Form>





    </>
    )
}