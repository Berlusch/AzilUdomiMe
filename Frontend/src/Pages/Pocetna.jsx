import { useEffect, useState } from "react";
import {  Col, Row } from "react-bootstrap";
import PasService from "../services/PasService";
import useLoading from "../hooks/useLoading";
import CountUp from "react-countup";




export default function Pocetna(){
    
const { showLoading, hideLoading } = useLoading();

const[psi, setPsi]=useState([])
const [udomljenihPasa, setUdomljenihPasa] = useState(0);
const [slobodniPsi, setSlobodniPsi] = useState([]);

async function dohvatiSlobodnePse() {
            try {
            const odgovor = await PasService.get();
            console.log(psi);
            setPsi(odgovor);         
            const filtriraniPsi = odgovor.filter(pas => 
                pas.statusNaziv === "slobodan" || pas.statusNaziv === "privremeni smještaj"
              );
        
        console.log("Filtrirani psi (slobodni):", filtriraniPsi);
        setSlobodniPsi(filtriraniPsi);
    } catch (e) {
        console.log(e);
    }
}

async function dohvatiBrojUdomljenihPasa() {
    try {
        const odgovor = await PasService.get(); // Dohvati sve pse
        const brojUdomljenih = odgovor.filter(pas => pas.statusNaziv === "udomljen").length;
        setUdomljenihPasa(brojUdomljenih);
    } catch (e) {
        console.log(e);
    }
}
    


async function ucitajPodatke() {
    showLoading();    
    await dohvatiSlobodnePse();
    await dohvatiBrojUdomljenihPasa();
    hideLoading();
  }


useEffect(()=>{
    ucitajPodatke()
},[]);



return (
    <>
      <h1 className="welcome-title">
        Dobro došli u aplikaciju Udomi me!
      </h1>
             
      <Row>
      <Col xs={6} sm={6} md={3} lg={6} xl={6} xxl={6}>
        <h4>Oni traže svoj dom:</h4>
        <div className="psiGrid">
            {psi && psi.filter(pas => pas.statusNaziv === "slobodan" || pas.statusNaziv === "privremeni smještaj").map((pas, index) => (
                <div key={index} className="pasItem">
                <img src={`/pas${pas.sifra}.jpg`} alt={pas.ime} className="pasSlika" />
                <p>{pas.ime}</p>
            </div>            
            ))}
        </div>
    </Col>         
  
        <Col xs={6} sm={6} md={9} lg={6} xl={6} xxl={6} className="text-end">
        <h4>Do sada je udomljeno:</h4>
        <div className="brojUdomljenihPasa">
            <CountUp start={0} end={udomljenihPasa} duration={10} separator="." />
        </div>
        <p>{udomljenihPasa === 1 ? 'udomljen pas' : (udomljenihPasa < 5 ? 'udomljena psa' : 'udomljenih pasa')}</p>
        <h4>Hvala udomiteljima ❤️</h4>
        </Col>
      </Row>
    </>
  );
    
}


