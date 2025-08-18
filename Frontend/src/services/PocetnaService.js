import { HttpService } from "./HttpService";


export async function getPsi(stranica) {
    try {
        const odgovor = await HttpService.get('/Pocetna/traziStranicenje/' + stranica);        
        
        return odgovor.data;          
       
        
    } catch (e) {
        console.error("Greška prilikom dohvaćanja podataka:", e);
        return {
            psi: [],
            
        };
    }
}

async function getPasPoSifri(sifra){
    return await HttpService.get('/Pocetna/pasPoSifri/'+sifra)
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return {greska:false, poruka: odgovor.data};
    })
    .catch((e)=>{})
}

async function getUkupnoStranica(){
    try {
        const odgovor = await HttpService.get('/Pocetna/izracunajUkupnoStranica/');
        console.log("ukupno stranica:", odgovor.data);     
        
        return odgovor.data;          
       
        
    } catch (e) {
        console.error("Greška prilikom dohvaćanja podataka:", e);        
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

async function postUpitForm(upitData) {
    try {        
        const odgovor = await HttpService.post('Pocetna/upitObrazac/', upitData);   
        return odgovor.data;        
    } catch (e) {
        console.error("Greška prilikom upisa podataka:", e);
        return { greska: true, poruka: "Došlo je do pogreške prilikom slanja upita." };
    }  
}   


export default {
        getPsi,
        getBrojUdomljenihPasa,
        getUkupnoStranica,
        getPasPoSifri,
        postUpitForm
    }