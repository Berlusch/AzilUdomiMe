import { useParams } from 'react-router-dom';
import { getPasBySifra } from '../services/PasService'; 

function PasDetalji() {
  const { sifra } = useParams(); // Dohvat šifre psa iz URL-a

  // Dohvati podatke o psu prema šifri
  const pas = getPasBySifra(sifra);

  return (
    <div className="pasDetalji">
      <h1>{pas.ime}</h1>
      <img src={`/pas${pas.sifra}.jpg`} alt={pas.ime} className="pasSlika" />
      <p>Broj čipa: {pas.brojCipa}</p>
      <p>Datum rođenja: {pas.datum_Rodjenja}</p>
      <p>Spol: {pas.spol}</p>
      <p>Opis: {pas.opis}</p>
      <p>Kastracija: {pas.kastracija}</p>
      <p>Status: {pas.statusNaziv}</p>
      </div>
  );
}

export default PasDetalji;