import { HttpService } from "./HttpService";


async function getPsi(stranica){
    return await HttpService.get('/Pocetna/traziStranicenje/' + stranica)
    .then((odgovor)=>{
        console.log("Dobiveni podaci:", odgovor.data);
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}


    export default {
        getPsi
    }