
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
import PsiBrisanje from './Pages/Psi/PsiBrisanje'



function App() {
  
  return (
    <>
      <Container>
        <NavbarEdunova />
        <Routes>
          <Route path={RouteNames.HOME} element={<Pocetna />} />
          <Route path={RouteNames.UDOMITELJ_PREGLED} element={<UdomiteljiPregled />} />
          <Route path={RouteNames.UDOMITELJ_NOVI} element={<UdomiteljiDodaj />}/>
          <Route path={RouteNames.UDOMITELJ_PROMJENA} element={<UdomiteljiPromjena />} />
          <Route path={RouteNames.UDOMITELJ_BRISANJE} element={<UdomiteljiBrisanje/>} />
          <Route path={RouteNames.PAS_PREGLED} element={<PsiPregled />} />
          <Route path={RouteNames.PAS_NOVI} element={<PsiDodaj />}/>
          <Route path={RouteNames.PAS_PROMJENA} element={<PsiPromjena />} />
          <Route path={RouteNames.PAS_BRISANJE} element={<PsiBrisanje/>} />
        </Routes>
        <hr/>
    

        <img src="/paw.png" alt="Logo" className="logo-centered" />
        
            
        
      </Container>
      <div style={{ textAlign: 'center', paddingRight: '30px' }}>
  &copy; Udomi me 2025.
</div>

    </>
      
  )
}

export default App
