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
        return {greska:false, poruka: odgovor.data};
    })
    .catch((e)=>{})
}

async function dodaj(Pas){
    return await HttpService.post('/Pas', Pas)
    .then(()=>{return{greska:false, poruka: 'Dodano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod dodavanja'}})
}

async function promijeni(sifra,Pas){
    return await HttpService.put('/Pas/'+sifra, Pas)
    .then(()=>{return{greska:false, poruka: 'Promijenjeno'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod promjene'}})
}

async function obrisi(sifra,Pas){
    return await HttpService.delete('/Pas/'+sifra, Pas)
    .then(()=>{return{greska:false, poruka: 'Obrisano'}})
    .catch(()=>{return{greska:true, poruka:'Problem kod brisanja'}})
}

async function traziPsa(uvjet){
    return await HttpService.get('/Pas/trazi/'+uvjet)
    .then((odgovor)=>{
        //console.table(odgovor.data);
        return {greska: false, poruka: odgovor.data}
    })
    .catch((e)=>{return {greska: true, poruka: 'Problem kod traženja polaznika'}})
}

export default{
    get,
    getBySifra,
    promijeni,
    dodaj,
    obrisi,
    traziPsa
}