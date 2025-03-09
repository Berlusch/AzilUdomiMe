
import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css'
import { Container } from 'react-bootstrap'
import NavbarEdunova from './Components/NavbarEdunova'
import { Route, Routes } from 'react-router-dom'
import { RouteNames } from './constants'
import Pocetna from './Pages/Pocetna'
import UdomiteljiPregled from './Pages/Udomitelji/UdomiteljiPregled'
import UdomiteljiDodaj from './Pages/Udomitelji/UdomiteljiDodaj'
import UdomiteljiPromjena from './Pages/Udomitelji/UdomiteljiPromjena'
import UdomiteljiBrisanje from './Pages/Udomitelji/UdomiteljiBrisanje'
import PsiPregled from './Pages/Psi/PsiPregled'
import PsiDodaj from './Pages/Psi/PsiDodaj'
import PsiPromjena from './Pages/Psi/PsiPromjena'
import StatusiPregled from './Pages/Statusi/StatusiPregled'
import StatusiDodaj from './Pages/Statusi/StatusiDodaj'
import StatusiPromjena from './Pages/Statusi/StatusiPromjena'
import UpitiPregled from './Pages/Upiti/UpitiPregled'
import UpitiDodaj from './Pages/Upiti/UpitiDodaj'
import UpitiPromjena from './Pages/Upiti/UpitiPromjena'

import EraDijagram from './Pages/Era/EraDijagram'
import Login from "./Pages/Login"
import useAuth from "./hooks/useAuth"
import NadzornaPloca from './Pages/NadzornaPloca'
import useError from "./hooks/useError"
import PasDetalji from './Pages/PasDetalji'





function App() {

  const { isLoggedIn } = useAuth();
  const { errors, prikaziErrorModal, sakrijError } = useError();
  
  return (
    <>
      <Container>
        <NavbarEdunova />
        <Routes>
        <Route path={RouteNames.HOME} element={<Pocetna />} />
        <Route path={RouteNames.DETALJI_PSA} element={<PasDetalji />} />
          {isLoggedIn ? (
        <>
          <Route path={RouteNames.NADZORNA_PLOCA} element={<NadzornaPloca />} />

          <Route path={RouteNames.UDOMITELJ_PREGLED} element={<UdomiteljiPregled />} />
          <Route path={RouteNames.UDOMITELJ_NOVI} element={<UdomiteljiDodaj />}/>
          <Route path={RouteNames.UDOMITELJ_PROMJENA} element={<UdomiteljiPromjena />} />
          <Route path={RouteNames.UDOMITELJ_BRISANJE} element={<UdomiteljiBrisanje/>} />
          
          <Route path={RouteNames.PAS_PREGLED} element={<PsiPregled />} />
          <Route path={RouteNames.PAS_NOVI} element={<PsiDodaj />} />
          <Route path={RouteNames.PAS_PROMJENA} element={<PsiPromjena />} />
          
          <Route path={RouteNames.UPIT_PREGLED} element={<UpitiPregled />} />
          <Route path={RouteNames.UPIT_NOVI} element={<UpitiDodaj />} />
          <Route path={RouteNames.UPIT_PROMJENA} element={<UpitiPromjena />} />
          
          <Route path={RouteNames.STATUS_PREGLED} element={<StatusiPregled />} />
          <Route path={RouteNames.STATUS_NOVI} element={<StatusiDodaj />} />
          <Route path={RouteNames.STATUS_PROMJENA} element={<StatusiPromjena />} />

          <Route path={RouteNames.ERA_DIJAGRAM} element={<EraDijagram/>} />
          
          </>
        ) : (
          <>
            <Route path={RouteNames.LOGIN} element={<Login />} />
          </>
        )}  
        </Routes>
        <hr/>
    

        <img src="/paw.png" alt="Logo" className="logo-centered" width="220" />


      </Container>
      <div style={{ textAlign: 'center', paddingRight: '30px' }}>
  &copy; Udomi me 2025.
  Kontakt: aziludomime@gmail.com
</div>

    </>
      
  )
}

export default App
