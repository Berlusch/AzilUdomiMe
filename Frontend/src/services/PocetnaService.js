import { HttpService } from "./HttpService";


export async function getPsi(stranica) {
    try {
        const odgovor = await HttpService.get('/Pocetna/traziStranicenje/' + stranica);
        console.log("Dobiveni podaci:", odgovor.data);     
        
        return {
            psi: odgovor.data.Psi,
            ukupnoStranica: odgovor.data.UkupnoStranica
        };
        
    } catch (e) {
        console.error("Greška prilikom dohvaćanja podataka:", e);
        return {
            psi: [],
            ukupnoStranica: 0
        };
    }
}

async function getBrojUdomljenihPasa(){
    return await HttpService.get('/Pocetna/traziUdomljenePse/')
    .then((odgovor)=>{
        console.log("Dobiveni podatci:", odgovor.data);
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}


    export default {
        getPsi,
        getBrojUdomljenihPasa
    }