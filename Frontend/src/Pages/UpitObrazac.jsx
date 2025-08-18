import { Button, Col, Form, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
import { RouteNames } from "../constants";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import emailjs from "emailjs-com";
import { useNavigate } from "react-router-dom";
import PocetnaService from "../services/PocetnaService";


export default function UpitObrazac() {
    const navigate = useNavigate();
    const { sifra } = useParams();
    const [pasIme, setPasIme] = useState("");
    const [pasBrojCipa, setPasBrojCipa] = useState("");
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        emailjs.init('2Zssg2RzCT6m4NeRA');  
        
    }, []);

   
    
    useEffect(() => {
        async function fetchPas() {
            const odgovor = await PocetnaService.getPasPoSifri(sifra);

            if (odgovor.greska) {
                setError(odgovor.poruka);
                setIsLoading(false);
            } else {
                setPasIme(odgovor.poruka.ime);
                setPasBrojCipa(odgovor.poruka.brojCipa)
                setIsLoading(false);
            }
        }

        fetchPas();
    }, [sifra]);

    async function odradiSubmit(e) {
    e.preventDefault();

    const podatci = new FormData(e.target);

    const today = new Date();
    const datum = `${today.getDate()}.${today.getMonth() + 1}.${today.getFullYear()}.`;

    const upitData = {
        ime: podatci.get("ime"),
        prezime: podatci.get("prezime"),
        adresa: podatci.get("adresa"),
        telefon: podatci.get("telefon"),
        email: podatci.get("email"),
        sadrzajUpita: podatci.get("sadrzajUpita"),
        pasIme: pasIme,
        datum: datum,
        pasBrojCipa: pasBrojCipa
    };

    const upitObrazacBaza = {
    PasSifra: Number(sifra),           // ako sifra dolazi kao string, pretvori u broj
    Ime: podatci.get("ime"),
    Prezime: podatci.get("prezime"),
    Email: podatci.get("email"),
    SadrzajUpita: podatci.get("sadrzajUpita")
};

    try {
        await emailjs.send('service_g6kbdlp', 'template_dtb2wpu', upitData, '2Zssg2RzCT6m4NeRA');
        console.log("Success: email poslan");

        await emailjs.send('service_g6kbdlp', 'template_es0hg7u', upitData, '2Zssg2RzCT6m4NeRA');
        console.log("Autoresponder poslan korisniku");

        const odgovor = await PocetnaService.postUpitForm(upitObrazacBaza);
        if (odgovor.greska) {
            alert("Došlo je do pogreške prilikom slanja upita u bazu: " + odgovor.poruka);
        } else {
            alert("Upit je uspješno poslan i spremljen!");
        }

        navigate(RouteNames.HOME);

    } catch (error) {
        console.error("Greška:", error);
        alert("Došlo je do pogreške prilikom slanja upita.");
    }
}

    return (
        <>
            <h2 className="naslov">Upit za psa {pasIme}</h2>
            <p>(sva polja su obvezna)</p>
            <Form onSubmit={odradiSubmit}>
                <Form.Group controlId="ime">
                    <Form.Label>Ime</Form.Label>
                    <Form.Control type="text" name="ime" required />
                </Form.Group>

                <Form.Group controlId="prezime">
                    <Form.Label>Prezime</Form.Label>
                    <Form.Control type="text" name="prezime" required />
                </Form.Group>

                <Form.Group controlId="adresa">
                    <Form.Label>Adresa</Form.Label>
                    <Form.Control type="text" name="adresa" required />
                </Form.Group>

                <Form.Group controlId="telefon">
                    <Form.Label>Telefon</Form.Label>
                    <Form.Control type="number" name="telefon" required />
                </Form.Group>

                <Form.Group controlId="email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="text" name="email" required />
                </Form.Group>

                <Form.Group controlId="sadrzajUpita">
                    <Form.Label>Sadržaj upita</Form.Label>
                    <Form.Control type="text" name="sadrzajUpita" required />
                </Form.Group>

                <hr />
                <Row>
                    <Col xs={6} sm={12} md={3} lg={6} xl={6} xxl={6}>
                        <Link
                            to={RouteNames.DETALJI_PSA}
                            className="btn btn-danger siroko"
                            style={{ backgroundColor: '#9c989a' }}
                        >
                            Odustani  
                            <svg xmlns="http://www.w3.org/2000/svg" width="22" height="24" fill="red" className="bi bi-x-lg" viewBox="0 0 16 16" stroke="red">
                                <g transform="translate(2, 0)">
                                    <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
                                </g>
                            </svg>
                        </Link>
                    </Col>

                    <Col xs={6} sm={12} md={9} lg={6} xl={6} xxl={6}>
                        <Button variant="success" type="submit" className="siroko" style={{ backgroundColor: '#7d3d9b' }}>
                            Pošalji upit  
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="26" fill="#00FF00" className="bi bi-check-lg" viewBox="0 0 16 16" stroke="#00FF00">
                                <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
                            </svg>
                        </Button>
                    </Col>
                </Row>
            </Form>
        </>
    );
}
