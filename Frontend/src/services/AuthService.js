import { HttpService } from './HttpService';

export async function logInService(podatci) {
  return await HttpService
    .post('/Autorizacija/token', podatci)
    .then((odgovor)=>{return  {greska: false, poruka: odgovor.data};})
    .catch((e)=>{ return {greska: true, poruka: 'Problem kod autorizacije '}});
}