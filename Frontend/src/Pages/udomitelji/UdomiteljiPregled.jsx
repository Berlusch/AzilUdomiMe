import { useEffect, useState } from "react"
import UdomiteljService from "../../services/UdomiteljService"
import { Button, Table } from "react-bootstrap";
//import { NumericFormat } from "react-number-format";
//import moment from "moment";
//import { GrValidate } from "react-icons/gr";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";



export default function UdomiteljiPregled(){

    const [udomitelji, setUdomitelji]= useState();
    const navigate=useNavigate();


    async function dohvatiUdomitelje(){
        const odgovor = await UdomiteljService.get()
        setUdomitelji(odgovor)

    }

    //hooks (kuka) se izvodi prilikom dolaska na stranicu Udomitelji
    useEffect(()=>{
        dohvatiUdomitelje();
        
    },[])

    return(
        <>

        <Link
        to={RouteNames.UDOMITELJ_NOVI}
        className="btn btn-success siroko"
        >Dodaj novog udomitelja</Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Ime</th>
                    <th>Prezime</th>
                    <th>Adresa</th>
                    <th>Telefon</th>
                    <th>Email</th>
                    <th style={{ width: "120px", textAlign: "center" }}>Akcija</th>
                </tr>

            </thead>
            <tbody>
                {udomitelji && udomitelji.map((udomitelj,index)=>(
                    <tr key={index}>
                        <td>
                            {udomitelj.ime}
                            
                        </td>
                        <td>
                            {udomitelj.prezime}
                        </td>
                        <td>
                            {udomitelj.adresa}
                        </td>
                        <td>
                            {udomitelj.telefon}
                        </td>
                        <td>
                            {udomitelj.email}
                        </td>  
                        <td style={{ display: "flex", gap: "10px" }}>
                            <Button
                            style={{ backgroundColor: '#7d3d9b', color: "white" }}
                            onClick={() => navigate(`/udomitelji/${udomitelj.sifra}`)}
                            >
                                Promjena
                            </Button>
                            
                            <Button
                            style={{ backgroundColor: '#9c989a', color: "white" }}
                            onClick={() => navigate(`/udomitelji/obrisi/`)}
                            >Brisanje
                            </Button>
                            </td> 
                                        
                    </tr>
                ))}
            </tbody>
        </Table>
        </>
    )
}