import { Route, Routes, Link } from 'react-router-dom';
import Produtos from "./pages/Produtos";
import Lavagem from "./pages/Lavagem";
import Cuidado from './pages/Cuidado';

function Home(){
  return(
    <div>
      <h1>Rotina Capilar</h1>
      <nav>
        <Link to="/produtos"><button>Produtos</button></Link>
        <Link to="/lavagem"><button>Lavagens</button></Link>
        <Link to="/cuidados"><button>Cuidados</button></Link>
      </nav>
    </div>
  );
}

function App() {
  return(
    <Routes>
      <Route path='/' element={<Home />}></Route>
      <Route path='/produtos' element={<Produtos />}></Route>
      <Route path='/lavagem' element={<Lavagem />}></Route>
      <Route path='/cuidados'element={<Cuidado />}></Route>
    </Routes>
  );
}

export default App;
