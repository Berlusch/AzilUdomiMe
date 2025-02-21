import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate} from "react-router-dom";
import { RouteNames } from "../../constants";
import Service from "../../services/PasService";
import moment from "moment";
import StatusService from "../../services/StatusService";
import { useEffect, useState } from 'react';

export default function PsiDodaj(){
    const navigate = useNavigate();   
    
    const [statusi, setStatusi]=useState([]);
    const [statusNaziv, setStatusNaziv]=useState('');
   
    async function dohvatiStatuse(){
        const odgovor=await StatusService.get();
        setStatusi(odgovor.poruka);
        setStatusNaziv(odgovor.poruka[{}].sifra)
    }

    useEffect(()=>{
        dohvatiStatuse();
        
        // eslint-disable-next-line react-hooks/exhaustive-deps
      },[]);

    async function dodaj(e){
        const odgovor= await Service.dodaj(e);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.PAS_PREGLED)
             

    }

    function obradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        const podatci = new FormData(e.target);

        dodaj(
            {            
 
                ime: podatci.get('ime'),
                brojcipa: podatci.get('brojCipa'),
                datum_rodjenja: moment.utc(podatci.get('datum_Rodjenja')),
                spol: podatci.get('spol'),
                opis: podatci.get('opis'),
                kastracija: podatci.get('kastracija')=='on' ? true : false,
                statusNaziv: podatci.get(statusNaziv)
                                
            }
             
        
        );
    }

    
    return(
    <>
    <h2 className="naslov">Dodavanje psa</h2>
    <Form onSubmit={obradiSubmit}>

        <Form.Group controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" required/>
        </Form.Group>

        <Form.Group controlId="brojCipa">
            <Form.Label>Broj čipa</Form.Label>
            <Form.Control type="text" name="brojCipa" required/>
        </Form.Group>

        <Form.Group controlId="datum_Rodjenja">
            <Form.Label>Datum rođenja</Form.Label>
            <Form.Control type="date" name="datum_Rodjenja" required/>
        </Form.Group>

        <Form.Group controlId="spol">
            <Form.Label>Spol (M/Ž)</Form.Label>
            <Form.Control type="text" name="spol" required/>
        </Form.Group>

        <Form.Group controlId="opis">
            <Form.Label>Opis</Form.Label>
            <Form.Control type="text" name="opis" required/>
        </Form.Group>

        <Form.Group controlId="kastracija">
            <Form.Check label="Kastracija" name="kastracija"/>
            </Form.Group>

        <Form.Group controlId="statusNaziv">
            <Form.Label>Status</Form.Label>
            <Form.Select 
            onChange={(e)=>{setStatusNaziv(e.target.value)}}
            >
            {statusi && statusi.map((s,index)=>(
              <option key={index} value={s.statusNaziv}>
                {s.sifra}
              </option>
            ))}
            </Form.Select>
          </Form.Group>

        
        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.PAS_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#9c989a' }}
                >Odustani  <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" class="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg></Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko" style={{ backgroundColor: '#7d3d9b' }}>
                    Dodaj Psa  <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" class="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
  <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
</svg>
                </Button>
            </Col>

        </Row>


    </Form>


    
    </>
    )

}