import { useEffect,useState } from "react"
import UdomiteljService from "../../services/UdomiteljService"
import { Tab, Table } from "react-bootstrap";


export default function UdomiteljiPregled(){

    const [udomitelji, setUdomitelji]= useState();



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
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Ime</th>
                    <th>Prezime</th>
                    <th>Adresa</th>
                    <th>Telefon</th>
                    <th>Email</th>
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
                    </tr>
                ))}
            </tbody>
        </Table>
        </>
    )
}