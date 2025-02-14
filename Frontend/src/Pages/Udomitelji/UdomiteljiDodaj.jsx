import { Col, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
import { RouteNames } from "../../constants";

export default function UdomiteljiDodaj(){

    
    return(
    <>
    Dodavanje udomitelja
    <Row>
        <Col>
            <Link
            to={RouteNames.UDOMITELJ_PREGLED}
            className="btn btn-danger siroko"
            >Odustani</Link>
        </Col>
    </Row>
    </>
    )
}