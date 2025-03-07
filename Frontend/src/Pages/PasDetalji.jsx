import { useNavigate, useParams, Link } from 'react-router-dom';
import PocetnaService from '../services/PocetnaService'; 
import { useState } from 'react';
import { RouteNames } from "../../constants";
import { Row, Col } from "react-bootstrap";

async function PasDetalji() {
  const [pas, setPas] = useState(null);
  const navigate = useNavigate();
  const routeParams = useParams();

  async function DohvatiPsaPoSifri(sifra) {
    try {
      const odgovor = await PocetnaService.getPasPoSifri(sifra);
      setPas(odgovor);
    } catch (e) {
      console.log(e);
    }
  }

  useEffect(() => {
    if (routeParams.sifra) {
      DohvatiPsaPoSifri(routeParams.sifra);
    }
  }, [routeParams.sifra]);

  if (!pas) {
    return <p>Učitavanje podataka o psu...</p>;
  }

  return (
    <>
      <Link to={RouteNames.DETALJI_PSA} className="btn btn-success siroko">
        {pas.ime}
      </Link>

      <Row>
        <Col xs={6} sm={6} md={3} lg={6} xl={6} xxl={6}>
          <h4>{pas.ime}</h4>
          <img
            src={`/pas/${pas.sifra}.jpg`}
            alt={pas.ime}
            className="pasDetaljiSlika"
          />
          <p>{pas.ime}</p>
        </Col>

        <Col xs={6} sm={6} md={9} lg={6} xl={6} xxl={6} className="text-end">
          <h4>Detalji psa {pas.ime}</h4>
          <div className="detaljiPsa">
            <p>Broj čipa: {pas.brojCipa}</p>
            <p>Datum rođenja: {pas.datum_Rodjenja}</p>
            <p>Spol: {pas.spol}</p>
            <p>Opis: {pas.opis}</p>
            <p>Kastracija: {pas.kastracija ? "Da" : "Ne"}</p>
            <p>Status: {pas.statusNaziv}</p>
          </div>
        </Col>
      </Row>
    </>
  );
}








    