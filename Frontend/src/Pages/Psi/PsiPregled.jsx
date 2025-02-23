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

    function kastracija(k){
        if(k) return 'green'
        return 'red'
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
                {psi && psi.map((pas,index)=>(
                    <tr key={index}>
                        <td>
                            {pas.ime}
                            
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
                            <GrValidate 
                            size={30}
                            color={kastracija(pas.kastracija)}
                            />                            
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