import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import Service from "../../services/UpitService";
import moment from "moment"
import PasService from "../../services/PasService";
import UdomiteljService from "../../services/UdomiteljService";
import{AsyncTypeahead} from 'react-bootstrap-typeahead';
import { useState } from "react";

export default function UpitiDodaj(){

    const navigate = useNavigate();   
    
    const [pasSifra, setPasSifra]=useState(1);
    const [pasIme, setPasIme]=useState('');
    const [pronadjeniPsi, setPronadjeniPsi]= useState([]);

    const [udomiteljSifra, setUdomiteljSifra]=useState(1);
    const [udomiteljIme, setUdomiteljIme]=useState('');
    const[pronadjeniUdomitelji, setPronadjeniUdomitelji]= useState([]);

    const typeaheadRefPas = useRef(null);
    const typeaheadRefUdomitelj = useRef(null);

    async function traziPsa(uvjet) {
        const odgovor= await PasService.traziPsa(uvjet);
        if(odgovor.greska){
        alert(odgovor.poruka);
        return;
        }
        setPronadjeniPsi(odgovor.poruka);
    }

    async function dodajPsa(e) {
       setPasSifra(e[0].sifra)
       setPasIme(e[0].ime)
       typeaheadRefPas.current.clear();
      }
          

        async function traziUdomitelja(uvjet){

            const odgovor= await UdomiteljService.traziUdomitelja(uvjet);
            if(odgovor.greska){
                alert(odgovor.poruka);
                return;
              }
              setPronadjeniUdomitelji(odgovor.poruka);

        }

        async function dodajUdomitelja(e) {
            setUdomiteljSifra(e[0].sifra)
            setUdomiteljIme(e[0].ime + ' ' + e[0].prezime)
            typeaheadRefUdomitelj.current.clear();
          }
    


    async function dodaj(e){
        const odgovor= await Service.dodaj(e);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.UPIT_PREGLED)     
       

    }

    function odradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        const podatci = new FormData(e.target);

        dodaj(
            {            
 
                pasSifra: pasSifra,
                udomiteljSifra: udomiteljSifra,
                datumUpita: moment.utc(podatci.get('datumUpita')),
                statusUpita: podatci.get('statusUpita'),
                napomene: podatci.get('napomene')
            }
             
        
        );
    }

    
    return(
    <>
    <h2 className="naslov">Dodavanje upita</h2>
    <Form onSubmit={odradiSubmit}>

    <Form.Group className='mb-3' controlId='uvjet'>
          <Form.Label>Traži psa</Form.Label>
            <AsyncTypeahead
            className='autocomplete'
            id='uvjet'
            emptyLabel='Nema rezultata'
            searchText='Tražim...'
            labelKey={(pas) => `${pas.ime}`}
            minLength={3}
            options={pronadjeniPsi}
            onSearch={traziPsa}
            placeholder='dio imena'
            renderMenuItemChildren={(pas) => (
              <>
                <span>
                   {pas.ime}
                </span>
              </>
            )}
            onChange={dodajPsa}
            ref={typeaheadRefPas}
            />
            <p>
                {pasIme}
            </p>
    </Form.Group>
      
        

    <Form.Group className='mb-3' controlId='uvjet'>
          <Form.Label>Traži udomitelja</Form.Label>
            <AsyncTypeahead
            className='autocomplete'
            id='uvjet'
            emptyLabel='Nema rezultata'
            searchText='Tražim...'
            labelKey={(udomitelj) => `${udomitelj.ime} ${udomitelj.prezime}`}
            minLength={3}
            options={pronadjeniUdomitelji}
            onSearch={traziUdomitelja}
            placeholder='dio imena'
            renderMenuItemChildren={(udomitelj) => (
              <>
                <span>
                   {udomitelj.ime}
                </span>
              </>
            )}
            onChange={dodajUdomitelja}
            ref={typeaheadRefUdomitelj}
            />
            <p>
                {udomiteljImePrezime}
            </p>
    </Form.Group>
        
        <Form.Group controlId="datumUpita">
                <Form.Label>Datum upita</Form.Label>
                <Form.Control type="date" name="datumUpita" />
            </Form.Group>

        <Form.Group controlId="statusUpita">
            <Form.Label>Status upita</Form.Label>
            <Form.Control type="text" name="statusUpita" required/>
        </Form.Group>

        <Form.Group controlId="napomene">
            <Form.Label>Napomene</Form.Label>
            <Form.Control type="text" name="napomene" required/>
        </Form.Group>

        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.UPIT_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#9c989a' }}
                >Odustani  <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" className="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg></Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko" style={{ backgroundColor: '#7d3d9b' }}>
                    Dodaj upit  <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" className="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
  <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
</svg>
                </Button>
            </Col>

        </Row>


    </Form>


    
    </>
    )

}