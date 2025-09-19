import { Route, Routes, Link } from 'react-router-dom';
import Produtos from "./pages/Produtos";

function Home(){
  return(
    <div>
      <h1>Rotina Capilar</h1>
      <nav>
        <Link to="/produtos"><button>Produtos</button></Link>
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
      <Route path='/cuidados'element={<h2>Pagina de Cuidados</h2>}></Route>
    </Routes>
  );
}

export default App;
