import { HttpService } from "./HttpService";


async function get(){
    return await HttpService.get('/Udomitelj')
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

export default{
    get,
    dodaj
}