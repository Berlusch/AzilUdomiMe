import { useEffect, useState } from "react"
import PasService from "../../services/PasService"
import { Button, Table } from "react-bootstrap";
import moment from "moment";
import { GrValidate } from "react-icons/gr";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";



export default function PsiPregled(){

    const navigate=useNavigate();
    const [psi, setPsi]= useState();
    


    async function dohvatiPse(){
        const odgovor = await PasService.get()
        setPsi(odgovor)

    }

    function obrisi(sifra) {
        const pas = psi.find(p => p.sifra === sifra); 
        const imePsa = pas.ime; 
    
        if (!confirm(`Jeste li sigurni da želite obrisati psa ${imePsa}?`)) {
            return;
        }
        obrisiPsa(sifra)
    }
    async function obrisiPsa(sifra) {
        const odgovor = await PasService.obrisi(sifra);
        //console.log(odgovor);
        if(odgovor.greska){
            alert(odgovor.poruka);
            return;
        }
        dohvatiPse();
    }

    
    //hooks (kuka) se izvodi prilikom dolaska na stranicu Psi
    useEffect(()=>{
        dohvatiPse();
        
    },[])

    
    function formatirajDatum(datum_Rodjenja){
        
        return moment.utc(datum_Rodjenja).format('DD. MM. YYYY.')
    }

    function kastracija(k) {
        return k ? <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" className="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
        <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
      </svg> : <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" className="bi bi-x-lg" viewBox="0 0 16 16" stroke="red"><g transform="translate(2, 0)">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/></g>
              </svg>;
    }

    return(
        <>

        <Link
        to={RouteNames.PAS_NOVI}
        className="btn btn-success siroko"
        >Dodaj novog psa</Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Ime</th>
                    <th>Slika</th>
                    <th>Broj čipa</th>
                    <th>Datum rođenja</th>
                    <th>Spol</th>
                    <th>Opis</th>
                    <th>Kastracija</th>
                    <th>Status</th>
                    <th style={{ width: "120px", textAlign: "center" }}>Opcije</th>
                </tr>

            </thead>
            

            <tbody>
                    {psi && psi.map((pas, index) => (
                    <tr key={index}>
                        <td className="pas-td">
                        {pas.ime}
                        </td>
                        <td>
                    {/* Ikona s hover efektom */}
                    <div className="ikona">
                        <img
                        src="ikonicaSlika.png"
                        alt={pas.ime}
                        width="40"
                        />
                        <div className="tooltip">
                        <img 
                            src={`/pas${pas.sifra}.jpg`}  
                            alt={pas.ime}
                            width="220" 
                        />
                    </div>
                    </div>
                    </td>
                        <td>
                            {pas.brojCipa}
                        </td>
                        <td>
                            {formatirajDatum(pas.datum_Rodjenja)}
                        </td>
                        <td>
                            {pas.spol}
                        </td>
                        <td>
                            {pas.opis}
                        </td>
                        <td className="sredina">
                        {kastracija(pas.kastracija)}
                        </td>
                        <td>
                            {pas.statusNaziv}
                        </td>
                        
                        <td style={{ display: "flex", gap: "10px" }}>
                            <Button
                            style={{ backgroundColor: '#7d3d9b', color: "white" }}
                            onClick={() => navigate(`/psi/${pas.sifra}`)}
                            >
                                Promjena
                            </Button>
                            
                            <Button
                            style={{ backgroundColor: '#9c989a', color: "white" }}
                            onClick={() => obrisi(pas.sifra)}>
                            
                                Brisanje
                            </Button>
                        </td> 
                                        
                    </tr>
                ))}
            </tbody>
        </Table>
        </>
    )
}