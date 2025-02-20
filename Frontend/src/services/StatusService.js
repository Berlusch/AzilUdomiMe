import { HttpService } from "./HttpService";


async function get(){
    return await HttpService.get('/Status')
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function getBySifra(sifra){
    return await HttpService.get('/Status/'+sifra)
    .then((odgovor)=>{
        //console.table(odgovor.data)
        return odgovor.data;
    })
    .catch((e)=>{})
}

async function dodaj(status){
    return HttpService.post('/Status', status)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function promijeni(sifra,status){
    return HttpService.put('/Status/'+sifra, status)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function obrisi(sifra){
    return await HttpService.delete('/Smjer/' + sifra)
    .then(()=>{
        return {greska: false, poruka: 'Obrisano'}
    })
}

    export default {
        get,
        obrisi,
        dodaj,
        getBySifra,
        promijeni
    }