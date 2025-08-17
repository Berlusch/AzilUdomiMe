import { useEffect, useState } from "react"
import UpitService from "../../services/UpitService"
import { Button, Table } from "react-bootstrap";
import moment from "moment"
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";


export default function UpitiPregled(){

    const [upiti, setUpiti]= useState();
    const navigate=useNavigate();


    async function dohvatiUpite(){
        const odgovor = await UpitService.get()
        setUpiti(odgovor)

    }

    function obrisi(sifra){
            if (!confirm(`Jeste li sigurni da želite obrisati upit?`)) {
                return;
            }
            obrisiUpit(sifra);
        }
        async function obrisiUpit(sifra) {
            const odgovor = await UpitService.obrisi(sifra);
            //console.log(odgovor);
            if(odgovor.greska){
                alert(odgovor.poruka);
                return;
            }
            dohvatiUpite();
        }
    
    //hooks (kuka) se izvodi prilikom dolaska na stranicu Upiti
    useEffect(()=>{
        dohvatiUpite();
        
    },[])

    function formatirajDatum(datum){
        if(datum==null){
            return 'Nije definirano';
        }
        return moment.utc(datum).format('DD. MM. YYYY.')
    }
    
    return(
        <>

        <Link
        to={RouteNames.UPIT_NOVI}
        className="btn btn-success siroko"
        >Dodaj novi upit</Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Pas</th>
                    <th>Udomitelj</th>
                    <th>Datum upita</th>
                    <th>Status upita</th>
                    <th>Sadržaj upita</th>
                    <th style={{ width: "120px", textAlign: "center" }}>Opcije</th>
                </tr>

            </thead>
            <tbody>
                {upiti && upiti.map((upit,index)=>(
                    <tr key={index}>
                        <td>{upit.pasIme}</td>
                        <td>{upit.udomiteljImePrezime}</td>
                        <td>{formatirajDatum(upit.datumUpita)}</td>
                        <td>{upit.statusUpita}</td>
                        <td>{upit.sadrzajUpita}</td>
                        <td style={{ display: "flex", gap: "10px" }}>
                            <Button
                            style={{ backgroundColor: '#7d3d9b', color: "white" }}
                            onClick={() => navigate(`/upiti/${upit.sifra}`)}
                            >
                                Promjena
                            </Button>
                            
                            <Button
                            style={{ backgroundColor: '#9c989a', color: "white" }}
                            onClick={() => obrisi(upit.sifra)}
                            >
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