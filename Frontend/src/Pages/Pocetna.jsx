import { useEffect, useState } from "react";
import {  Col, Row } from "react-bootstrap";
import useLoading from "../hooks/useLoading";
import CountUp from "react-countup";
import PocetnaService from "../services/PocetnaService";
import { Link } from "react-router-dom";




export default function Pocetna(){
    
const { showLoading, hideLoading } = useLoading();

const [brojUdomljenihPasa, setBrojUdomljenihPasa] = useState(0);
const [slobodniPsi, setSlobodniPsi] = useState([]);
const [stranica, setStranica] = useState(1);
const [ukupnoStranica, setUkupnoStranica]=useState(0)


async function dohvatiSlobodnePse(stranica) {
    try {
        const odgovor = await PocetnaService.getPsi(stranica);          
            setSlobodniPsi(odgovor.psi);  
                           
        } catch (e) {
            console.log(e);
        }
    } 
    
async function dohvatiUkupnoStranica() {
        try {
            const odgovor = await PocetnaService.getUkupnoStranica(); 
            setUkupnoStranica(odgovor);
            console.log("ukupno stranica",ukupnoStranica)
        } catch (e) {
            console.log(e);
        }
    }    


async function dohvatiBrojUdomljenihPasa() {
    try {
        const odgovor = await PocetnaService.getBrojUdomljenihPasa(); 
        setBrojUdomljenihPasa(odgovor);
    } catch (e) {
        console.log(e);
    }
}   




async function ucitajPodatke() {
    showLoading();    
    await dohvatiSlobodnePse(stranica);
    dohvatiUkupnoStranica();   
    await dohvatiBrojUdomljenihPasa(); 
    
    hideLoading();
  }


useEffect(()=>{
    ucitajPodatke()
},[]);

async function sljedeca()  {    
    
        setStranica(stranica + 1);
        dohvatiSlobodnePse(stranica + 1);    
    
};
async function prethodna()  {    
    
    setStranica(stranica -1);
    dohvatiSlobodnePse(stranica -1);    

};


return (
    <>
      <h1 className="welcome-title">Dobro došli u aplikaciju Udomi me!</h1>

      <Row>
        <Col xs={12} sm={12} md={9} lg={6} xl={6} xxl={6}>
          <h4>Oni traže svoj dom:</h4>
          <div className="psiGrid">
              {slobodniPsi &&
                slobodniPsi.map((pas) => (
              <div key={pas.sifra} className="pasItem">
              <Link to={`/detalji-psa/${pas.sifra}`} className="pasLink">
                <img src={`/pas${pas.sifra}.jpg`} alt={pas.ime} className="pasSlika" />
              </Link>
              <p className="pasIme">{pas.ime}</p>
          </div>
      ))}
          </div>

          {stranica > 1 && (
            <Link className="greyButton" onClick={prethodna}>
              Prethodna
            </Link>
          )}

          {stranica < ukupnoStranica && (
            <Link className="purpleButton" onClick={sljedeca}>
              Sljedeća
            </Link>
          )}
        </Col>

        <Col xs={12} sm={12} md={3} lg={6} xl={6} xxl={6} className="text-end">
          <h4>Do sada je udomljeno:</h4>
          <div className="brojUdomljenihPasa">
            <CountUp start={0} end={brojUdomljenihPasa} duration={10} separator="." />
          </div>
          <p>
            {brojUdomljenihPasa === 1
              ? "udomljen pas"
              : brojUdomljenihPasa < 5
              ? "udomljena psa"
              : "udomljenih pasa"}
          </p>
          <h4>Hvala udomiteljima ❤️</h4>
        </Col>
      </Row>
    </>
  );
}


