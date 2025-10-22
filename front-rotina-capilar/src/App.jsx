import { Route, Routes, Link } from 'react-router-dom';
import Home from './pages/Home';
import Produtos from "./pages/Produtos";
import Lavagens from "./pages/Lavagens";
import Cuidados from './pages/Cuidados';

function App() {
  return(
    <Routes>
      <Route path='/' element={<Home />}></Route>
      <Route path='/produtos' element={<Produtos />}></Route>
      <Route path='/lavagens' element={<Lavagens />}></Route>
      <Route path='/cuidados'element={<Cuidados />}></Route>
    </Routes>
  );
}

export default App;
