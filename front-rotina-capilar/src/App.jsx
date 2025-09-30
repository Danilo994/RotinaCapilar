import { Route, Routes, Link } from 'react-router-dom';
import Produtos from "./pages/Produtos";
import Lavagens from "./pages/Lavagens";
import Cuidados from './pages/Cuidados';
import Avaliacoes from './pages/Avaliacoes';

function Home(){
  return(
    <div>
      <h1>Rotina Capilar</h1>
      <nav>
        <Link to="/produtos"><button>Produtos</button></Link>
        <Link to="/lavagens"><button>Lavagens</button></Link>
        <Link to="/cuidados"><button>Cuidados</button></Link>
        <Link to="/avaliacoes"><button>Avaliações</button></Link>
      </nav>
    </div>
  );
}

function App() {
  return(
    <Routes>
      <Route path='/' element={<Home />}></Route>
      <Route path='/produtos' element={<Produtos />}></Route>
      <Route path='/lavagens' element={<Lavagens />}></Route>
      <Route path='/cuidados'element={<Cuidados />}></Route>
      <Route path='/avaliacoes'element={<Avaliacoes />}></Route>
    </Routes>
  );
}

export default App;
