import { HttpService } from "./HttpService";


async function get(){
    return await HttpService.get('/Pas')
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function getBySifra(sifra){
    return await HttpService.get('/Pas/'+sifra)
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function dodaj(pas){
    return await HttpService.post('/Pas', pas)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function promijeni(sifra,pas){
    return HttpService.put('/Pas/'+sifra, pas)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function obrisi(sifra,pas){
    return HttpService.delete('/Pas/'+sifra, pas)
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