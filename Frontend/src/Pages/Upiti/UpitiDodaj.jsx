import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import Service from "../../services/UpitService";
import moment from "moment"
import PasService from "../../services/PasService";
import UdomiteljService from "../../services/UdomiteljService";
import { useEffect, useState } from 'react';
import{AsyncTypeahead} from 'react-bootstrap-typeahead';

export default function UpitiDodaj(){

    const navigate = useNavigate();   
    
    const [psi,setPsi]=useState([]);
    const [pasSifra, setPasSifra]=useState({});
    const [pronadeniPsi, setPronadeniPsi]= useState([]);

    const [udomitelji,setUdomitelji]=useState([]);
    const [udomiteljSifra, setUdomiteljSifra]=useState({});
    //const[pronadeniUdomitelji, setPronadeniUdomitelji]= useState({});

    async function dohvatiPse(){
            try {
                const odgovor = await PasService.get();
                console.log("Odgovor:", odgovor);  // Provjeri cijeli odgovor
        
                if (Array.isArray(odgovor) && odgovor.length > 0) {
                    setPsi(odgovor);  // Postavi niz pasa
                    setPasSifra(odgovor[0]?.sifra);  
                } else {
                    console.error("Nema pasa u odgovoru ili odgovor nije niz");
                }
            } catch (error) {
                console.error("Greška prilikom dohvaćanja pasa:", error);
            }
        }

    async function dohvatiUdomitelje(){
            try {
                const odgovor = await UdomiteljService.get();
                console.log("Odgovor:", odgovor);  // Provjeri cijeli odgovor
        
                if (Array.isArray(odgovor) && odgovor.length > 0) {
                    setUdomitelji(odgovor);  // Postavi niz udomitelja
                    setUdomiteljSifra(odgovor[1]?.sifra);  
                        console.log("Postavljen udomitelj:", odgovor[0]?.ime, odgovor[0]?.prezime);
                } 
                else {
                    console.error("Nema udomitelja u odgovoru ili odgovor nije niz");
                }
            } catch (error) {
                console.error("Greška prilikom dohvaćanja udomitelja:", error);
            }
        }
    

        async function traziPsa(uvjet) {
            const odgovor= await PasService.traziPsa(uvjet);
            if(odgovor.greska){
                alert(odgovor.poruka);
                return;
              }
              setPronadeniPsi(odgovor.poruka);
            }

        

        async function traziUdomitelja(uvjet){

            const odgovor= await UpitService.traziUdomitelja(uvjet);
            if(odgovor.greska){
                alert(odgovor.poruka);
                return;
              }
              setPronadeniUdomitelji(odgovor.poruka);

        }
    }


    useEffect(()=>{
            dohvatiPse();
            dohvatiUdomitelje();           
            
          },[]);

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
            labelKey={(pas) => `${pas.prezime} ${pas.ime}`}
            minLength={3}
            options={pronadeniPsi}
            onSearch={traziPsa}
            placeholder='dio imena ili prezimena'
            renderMenuItemChildren={(pas) => (
              <>
                <span>
                   {pas.ime}
                </span>
              </>
            )}
            onChange={dodajPsa}
            ref={typeaheadRef}
            />
          </Form.Group>
      
        

        <Form.Group className='mb-3' controlId='udomiteljImePrezime'>
            <Form.Label>Udomitelj</Form.Label>
            <Form.Select 
            onChange={(e)=>{setUdomiteljSifra(e.target.value)}}
            >
            {udomitelji && udomitelji.map((u,sifra)=>(
            <option key={sifra} value={u.sifra}>
            {u.ime +" "+ u.prezime}
            </option>
            ))}
            </Form.Select>
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

