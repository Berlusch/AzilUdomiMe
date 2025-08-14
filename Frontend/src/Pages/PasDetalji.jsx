import { useNavigate, useParams, Link } from 'react-router-dom';
import PocetnaService from '../services/PocetnaService'; 
import { useEffect, useState } from 'react';
import { RouteNames } from "../constants";
import { Row, Col } from "react-bootstrap";
import moment from 'moment';

export default function PasDetalji() {
  const [pas, setPas] = useState({});   
  const navigate = useNavigate();
  const routeParams = useParams();
  const { sifra } = useParams(0);
console.log(sifra);

  async function DohvatiPsaPoSifri(sifra) {
    try {
      const odgovor = await PocetnaService.getPasPoSifri(sifra);
      setPas(odgovor.poruka);      
    } catch (e) {
      console.log(e);
    }
  }
  

  useEffect(() => {
    if (routeParams.sifra) {
      DohvatiPsaPoSifri(routeParams.sifra);
    }
  }, []);

  function formatirajDatum(datum_Rodjenja){
          
          return moment.utc(datum_Rodjenja).format('DD. MM. YYYY.')
      }

   
  if (!pas) {
    return <p>Učitavanje podataka o psu...</p>;
  }

  return (
    <>
      <div className="btn btn-success siroko1">
        {pas.ime}
      </div>

      <Row>
        <Col xs={12} sm={12} md={3} lg={6} xl={6} xxl={6}className="text-end">
                 
          <img
            src={`/pas${sifra}.jpg`}
            alt={pas.ime}
            className="pasDetaljiSlika"
          />
          <div className="text-start">
        <Link to={RouteNames.HOME}className="listaPasa" >Povratak na listu pasa</Link>
        
        </div>
                  
        </Col>

        <Col xs={12} sm={12} md={9} lg={6} xl={6} xxl={6} className="text-start">
          <h4>Detalji psa {pas.ime}</h4>
          <div className="detaljiPsa">
            <p>Broj čipa: {pas.brojCipa}</p>
            <p>Datum rođenja: {formatirajDatum(pas.datum_Rodjenja)}</p>
            <p>
  Spol: 
  <span style={{ fontSize: "22px", color: pas.spol === "ženski" ? "red" : "blue"}}>
    {pas.spol === "ženski" ? "  ♀" : "  ♂"}
  </span> 
  {pas.spol === "ženski" ? " ženski" : " muški"}
</p>
            <p>Opis: {pas.opis}</p>
            <p>Kastracija: {pas.kastracija ? "Da" : "Ne"}</p>
            <p>Status: {pas.statusNaziv}</p>  
            {pas.statusNaziv === "na liječenju" && (
            <p>Napomena: pas će biti dostupan za udomljenje po završetku liječenja. Za više informacija molimo pošaljite upit.</p>
          )}
                  
            <br/> 
                   
            <Link 
            to={`/upit-obrazac/${pas.sifra}`}  
            className="posaljiUpit"
          >
            Pošalji upit
          </Link>

        </div>      
          
        </Col>
      </Row>
    </>
  );
}








    