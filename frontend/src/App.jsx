
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
        </Routes>
        <hr/>
    

        <img src="/logosrednji.jpg" alt="Logo" className="logo-centered" />
        
            
        
      </Container>
      &copy; Bernarda Lusch 2025. 
    </>
      
  )
}

export default App
