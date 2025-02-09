
import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css'
import { Container } from 'react-bootstrap'
import NavbarEdunova from './Components/NavbarEdunova'
import { Route, Routes } from 'react-router-dom'
import { RouteNames } from './constants'
import Pocetna from './Pages/Pocetna'
import UdomiteljiPregled from './Pages/Udomitelji/UdomiteljiPregled'




function App() {
  

  return (
    <>
<Container>
  <NavbarEdunova />
  <Routes>
    <Route path={RouteNames.HOME} element={<Pocetna />} />
    <Route path={RouteNames.UDOMITELJ_PREGLED} element={<UdomiteljiPregled />} />
  </Routes>
  <hr/>
  

      <img src="/logoveliki.svg" alt="Opis slike" />

      
   
      
      
</Container>

&copy; Udomi me 2025.
    

    
     
      
      
    </>
  )
}

export default App
