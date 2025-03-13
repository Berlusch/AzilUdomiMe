import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import moment from "moment";
import Service from "../../services/UpitService";
import { useEffect, useState } from "react";
import UdomiteljService from '../../services/UdomiteljService';
import PasService from '../../services/PasService';

export default function UpitiPromjena(){

    const navigate = useNavigate();
    const routeParams= useParams();

    const[udomitelji, setUdomitelji]=useState([]);
    const[udomiteljSifra, setUdomiteljSifra]=useState(1);
    
    const[psi, setPsi]=useState([]);
    const[pasSifra, setPasSifra]=useState(1);

    const [upit,setUpit]= useState({});
    const[upiti, setUpiti]=useState([])
    const[statusUpita, setStatusUpita]=useState({})

    async function dohvatiUdomitelje(){
        const odgovor=await UdomiteljService.get();
        setUdomitelji(odgovor)
    }

    async function dohvatiPse(){
        const odgovor=await PasService.get();
        setPsi(odgovor);
       
    }

    async function dohvatiUpite(){
        const odgovor = await Service.get();
        setUpiti(odgovor);
    }

    async function dohvatiUpit(){
        const odgovor=await Service.getBySifra(routeParams.sifra);
       
        let u=odgovor;
        setStatusUpita(u.statusUpita)
        setUpit(u);
        setUdomiteljSifra(u.udomiteljSifra);
        setPasSifra(u.pasSifra);
        u.datumUpita = moment.utc(u.datumUpita).format('yyyy-MM-DD');
    }
    async function dohvatiInicijalnePodatke(){
        await dohvatiUdomitelje();
        await dohvatiPse();
        await dohvatiUpit();
        await dohvatiUpite();
    }

    
    useEffect(()=>{
        dohvatiInicijalnePodatke();        
    },[])

    async function promijeni(upit){
        const odgovor= await Service.promijeni(routeParams.sifra,upit);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        navigate(RouteNames.UPIT_PREGLED)
       
        
    }

    function OdradiSubmit(e){ // e je event
        e.preventDefault(); //nemoj odraditi zahtjev na server na standardni način
        
        let podatci = new FormData(e.target);

        promijeni(
            {           
 
                pasSifra: pasSifra,
                udomiteljSifra: udomiteljSifra,
                datumUpita: moment.utc(podatci.get('datumUpita')),
                statusUpita: statusUpita,
                napomene: podatci.get('napomene')
            }
                
        );
    }

    
    return(
    <>
    <h2 className="naslov">Promjena upita</h2>
    <Form onSubmit={OdradiSubmit}>

    <Form.Group className='mb-3' controlId='statusNaziv'>
            <Form.Label>Pas</Form.Label>
            <Form.Select
            value={pasSifra}
            onChange={(e)=>{setPasSifra(e.target.value)}}
            >
            {psi && psi.map((s,index)=>(
              <option key={index} value={s.sifra}>
                {s.ime}
              </option>
            ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className='mb-3' controlId='statusNaziv'>
            <Form.Label>Udomitelj</Form.Label>
            <Form.Select
            value={udomiteljSifra}
            onChange={(e)=>{setUdomiteljSifra(e.target.value)}}
            >
            {udomitelji && udomitelji.map((s,index)=>(
              <option key={index} value={s.sifra}>
                {s.ime} {s.prezime}
              </option>
            ))}
            </Form.Select>
          </Form.Group>
      

            <Form.Group controlId="datumUpita">
                    <Form.Label>Datum upita</Form.Label>
                    <Form.Control type="date" name="datumUpita" required
                    defaultValue={upit.datumUpita} readOnly/>
                </Form.Group>

                <Form.Group className='mb-3' controlId='statusUpita'>
                <Form.Label>Status upita</Form.Label>
                <Form.Select onChange={(e) => setStatusUpita(e.target.value)} required>                    
                    <option value="zaprimljen">zaprimljen</option>
                    <option value="u obradi">u obradi</option>
                    <option value="obrađen">obrađen</option>                    

                    {upiti && 
                        upiti
                            .filter((s) => s.statusUpita !== "zaprimljen" && s.statusUpita !== "u obradi" && s.statusUpita !== "obrađen")
                            .map((s, index, self) => (
                                // Filtriranje duplića
                                self.findIndex((item) => item.statusUpita === s.statusUpita) === index && (
                                    <option key={index} value={s.statusUpita}>
                                        {s.statusUpita}
                                    </option>
                                )
                            ))
                    }
                </Form.Select>
            </Form.Group>

            <Form.Group controlId="napomene">
                <Form.Label>Napomene</Form.Label>
                <Form.Control type="text" name="napomene" required
                defaultValue={upit.napomene}/>
            </Form.Group>

        <hr/>
    

        <Row>
            <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                <Link
                to={RouteNames.UPIT_PREGLED}
                className="btn btn-danger siroko"
                style={{ backgroundColor: '#9c989a' }}
                >Odustani<svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" className="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg> </Link>
            </Col>

            <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                <Button variant="success" type="submit" className="siroko"style={{ backgroundColor: '#7d3d9b' }}>
                    Promijeni upit <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" className="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
  <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
</svg>
                </Button>
            </Col>

        </Row>


    </Form>





    </>
    )
}