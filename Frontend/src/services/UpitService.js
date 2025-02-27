import { HttpService } from "./HttpService";


async function get(){
    return await HttpService.get('/Upit')
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function getBySifra(sifra){
    return await HttpService.get('/Upit/'+sifra)
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function dodaj(upit) {
    try {
        await HttpService.post('/Upit', upit);
        return { greska: false, poruka: 'Dodano' };
    } catch (error) {
        console.error("Greška kod dodavanja:", error); // Dodaj detaljnu poruku
        return { greska: true, poruka: 'Problem kod dodavanja' };
    }
}


async function promijeni(sifra,upit){
    return await HttpService.put('/Upit/'+sifra, upit)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function obrisi(sifra,upit){
    return await HttpService.delete('/Upit/'+sifra, upit)
    .then(()=>{return{greska:false, poruka: 'Obrisano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod brisanja.'}})
}



export default{
    get,
    getBySifra,
    promijeni,
    dodaj,
    obrisi
    
}

