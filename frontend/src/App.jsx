
import "bootstrap/dist/css/bootstrap.min.css"
import './App.css'
import NavbarEdunova from '.components/NavbarEdunova'
import { Container } from 'react-bootstrap'
import NavbarEdunova from './Components/NavbarEdunova'
import {Route, Routes} from 'react-router-dom'
import{RouteNames} from './constans'
import Pocetna from './Pages/Pocetna'



function App() {
  

  return (
    <>
<Container>
  <NavbarEdunova />
  <Routes>
    <Route path={RouteNames.HOME} element={<Pocetna />} />
    <Route path={RouteNames.UDOMITELJ.PREGLED} element={<UdomiteljiPregled />} />
  </Routes>
  <hr/>
  &copy; Udomi me 2025.

      <img src="/logoveliki.svg" alt="Opis slike" />
      

</Container>
    

    
     
      
      
    </>
  )
}

export default App
