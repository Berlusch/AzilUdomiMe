import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate} from "react-router-dom";
import { RouteNames } from "../../constants";
import Service from "../../services/PasService";
import moment from "moment";
import StatusService from "../../services/StatusService";
import { useEffect, useState } from 'react';

export default function PsiDodaj(){
    const navigate = useNavigate();   
    
    const [statusi,setStatusi]=useState([]);
    const [statusSifra, setStatusSifra]=useState();
    const[psi, setPsi]=useState([])
    const [pasSpol, setPasSpol]=useState({});
   
    async function dohvatiStatuse(){
        try {
            const odgovor = await StatusService.get();
            console.log("Odgovor:", odgovor);  // Provjeri cijeli odgovor
    
            if (Array.isArray(odgovor) && odgovor.length > 0) {
                setStatusi(odgovor);  // Postavi niz statusa
                setStatusSifra(odgovor[1]?.sifra);  // Sigurno pristupanje drugom statusu (indeks 1)
            } else {
                console.error("Nema statusa u odgovoru ili odgovor nije niz");
            }
        } catch (error) {
            console.error("Greška prilikom dohvaćanja statusa:", error);
        }
    }

    async function dohvatiSpolove(){
        try {
            const odgovor = await Service.get();
            console.log("Odgovor:", odgovor);  // Provjeri cijeli odgovor
                if (Array.isArray(odgovor) && odgovor.length > 0) {
                setPsi(odgovor);  
                setPasSpol(odgovor[0].spol);  
            } else {
                console.error("Nema spola u odgovoru ili odgovor nije niz");
            }
        } catch (error) {
            console.error("Greška prilikom dohvaćanja spola:", error);
        }

    }

    useEffect(()=>{
        dohvatiStatuse();  
        dohvatiSpolove();     
       
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
                spol: pasSpol,
                opis: podatci.get('opis'),
                kastracija: podatci.get('kastracija')=='on' ? true : false,
                statusSifra: parseInt(statusSifra)
                                
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

        <Form.Group className='mb-3' controlId="spol">
            <Form.Label>Spol</Form.Label>
            <Form.Select 
                value={pasSpol} // Dodajte value da bi bila kontrolirana komponenta
                onChange={(e) => { setPasSpol(e.target.value) }}
                required // Dodajte required atribut
            >
                <option value="">Odaberite spol</option>
                <option value="muški">muški</option>
                <option value="ženski">ženski</option>
                {psi && psi.filter((s) => s.spol !== "muški" && s.spol !== "ženski").map((s, index) => (
                    <option key={index} value={s.spol}>
                        {s.spol}
                    </option>
                ))}
            </Form.Select>
        </Form.Group>

        <Form.Group controlId="opis">
            <Form.Label>Opis</Form.Label>
            <Form.Control type="text" name="opis" required/>
        </Form.Group>

        <Form.Group controlId="kastracija">
            <Form.Check label="Kastracija" name="kastracija"/>
            </Form.Group>

            <Form.Group className='mb-3' controlId='status'>
            <Form.Label>Status</Form.Label>
            <Form.Select 
            onChange={(e)=>{setStatusSifra(e.target.value)}}
            >
            <option value="">Odaberite status</option>
            {statusi && statusi.map((s,index)=>(
              <option key={index} value={s.sifra}>
                {s.naziv}
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
                >Odustani  <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" className="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg></Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko" style={{ backgroundColor: '#7d3d9b' }}>
                    Dodaj Psa  <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" className="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
  <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
</svg>
                </Button>
            </Col>

        </Row>


    </Form>


    
    </>
    )

}