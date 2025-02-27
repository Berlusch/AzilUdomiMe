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
    .then(()=>{return{greska:false, poruka: 'Promijenjeno'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod promjene'}})
}

async function obrisi(sifra,udomitelj){
    return HttpService.delete('/Udomitelj/'+sifra, udomitelj)
    .then(()=>{return{greska:false, poruka: 'Obrisano'}})
    .catch(()=>{return{greska:true, poruka:'Udomitelj ima aktualan upit te ga stoga nije moguće obrisati.'}})
}

async function traziUdomitelja(uvjet){
    return await HttpService.get('/Udomitelj/trazi/'+uvjet)
    .then((odgovor)=>{
        //console.table(odgovor.data);
        return {greska: false, poruka: odgovor.data}
    })
    .catch((e)=>{return {greska: true, poruka: 'Problem kod traženja udomitelja.'}})
}

export default{
    get,
    getBySifra,
    promijeni,
    dodaj,
    obrisi,
    traziUdomitelja
}

