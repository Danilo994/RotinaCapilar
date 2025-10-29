import { useEffect, useState } from "react";
import api from "../services/api";
import Header from "../components/Header";
import "../styles/tables.css";

function Produtos(){
    const [produtos, setProdutos] = useState([]);
    const [produtoSelecionado, setProdutoSelecionado] = useState(null);
    const [form, setForm] = useState({nomeProduto: ""});

    useEffect(() => {
        carregaProdutos();
    }, []);

    async function carregaProdutos(){
        const response = await api.get("/Produtos");
        setProdutos(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.name});
    }

    async function handleSalvar() {
        if(produtoSelecionado){
            await api.put("/Produtos",  { ...form, idProduto: produtoSelecionado.idProduto});
        } else{
            await api.post("/Produtos", form);
        }
        setForm({nomeProduto: ""});
        setProdutoSelecionado(null);
        carregaProdutos();
    }

    function handleEditar(produto){
        setProdutoSelecionado(produto);
        setForm({nomeProduto: produto.nomeProduto});
    }

    async function handleExcluir(id) {
        if(!window.confirm("Deseja excluir este produto?")) return;
        await api.delete(`/Produtos/${id}`);
    }

    return (
      <>
        <Header />
        <div className="page-container">
          <h2 className="page-title">Gerenciar Produtos</h2>
          <div className="form-container">
            <input
              type="text"
              name="nomeProduto"
              value={form.nomeProduto}
              onChange={handleChange}
              placeholder="Nome do Produto"
            />
            <button onClick={handleSalvar}>
              {produtoSelecionado ? "Salvar Edição" : "Adicionar"}
            </button>
            {produtoSelecionado && (
              <button className="cancelar" onClick={() => { setProdutoSelecionado(null); setForm({ nomeProduto: "" }); }}>
                Cancelar
              </button>
            )}
          </div>

          <table className="table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Nome Produto</th>
                <th>Editar</th>
                <th>Excluir</th>
              </tr>
            </thead>
            <tbody>
              {produtos.length === 0 ? (
                <tr>
                  <td colSpan="4" className="empty-row">Nenhum produto encontrado</td>
                </tr>
              ) : (
                produtos.map((p) => (
                  <tr key={p.idProduto}>
                    <td>{p.idProduto}</td>
                    <td>{p.nomeProduto}</td>
                    <td>
                      <button className="edit" onClick={() => handleEditar(p)}>Editar</button>
                    </td>
                    <td>
                      <button className="delete" onClick={() => handleExcluir(p.idProduto)}>Excluir</button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </>
    );
}

export default Produtos;