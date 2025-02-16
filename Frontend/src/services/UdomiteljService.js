import { HttpService } from "./HttpService";


async function get(){
    return await HttpService.get('/Udomitelj')
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function getBySifra(sifra){
    return await HttpService.get('/Udomitelj/'+sifra)
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function dodaj(udomitelj){
    return HttpService.post('/Udomitelj', udomitelj)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function promijeni(sifra,udomitelj){
    return HttpService.put('/Udomitelj/'+sifra, udomitelj)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function obrisi(sifra){
    return HttpService.delete(`/Udomitelj/${sifra}`)
    .then(()=>{return{greska:false, poruka: 'Obrisano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod brisanja'}})
}

export default{
    get,
    getBySifra,
    promijeni,
    dodaj,
    obrisi
}