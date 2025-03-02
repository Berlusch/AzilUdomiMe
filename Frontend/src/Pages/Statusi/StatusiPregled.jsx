import { useEffect, useState } from "react"
import StatusService from "../../services/StatusService"
import { Button, Table } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";


export default function StatusiPregled(){

    const [statusi, setStatusi]= useState();
    const navigate=useNavigate();


    async function dohvatiStatuse(){
        const odgovor = await StatusService.get()
        setStatusi (odgovor)

    }

   
    //hooks (kuka) se izvodi prilikom dolaska na stranicu Udomitelji
    useEffect(()=>{
        dohvatiStatuse();
        
    },[])

        
    function obrisi(sifra){
        if(!confirm('Sigurno želite obrisati status?')){
            return;
        }
        brisanjeStatusa(sifra)
    }

    async function brisanjeStatusa(sifra) {
        
        const odgovor = await StatusService.obrisi(sifra);
        if(odgovor.greska){
            alert(odgovor.poruka)
            return
        }
        dohvatiStatuse();
    }

    return(
        <>

        <Link
        to={RouteNames.STATUS_NOVI}
        className="btn btn-success sirokostatus"
        >Dodaj novi status</Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Naziv</th>
                    
                </tr>

            </thead>
            <tbody>
                {statusi && statusi.map((status,index)=>(
                    <tr key={index}>
                        <td>
                            {status.naziv}
                        </td>  
                        
                        <td style={{ display: "flex", gap: "10px" }}>
                            <Button
                            style={{ backgroundColor: '#7d3d9b', color: "white" }}
                            onClick={() => navigate(`/statusi/${status.sifra}`)}
                            >
                                Promjena
                            </Button>
                            
                            <Button
                            style={{ backgroundColor: '#9c989a', color: "white" }}
                            onClick={()=>obrisi(status.sifra)}
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