import { useEffect, useState } from "react";
import api from "../services/api";

function Produtos(){
    const [produtos, setProdutos] = useState([]);
    const [produtoSelecionado, setProdutoSelecionado] = useState(null);
    const [form, setForm] = useState({nomeProduto: ""});

    useEffect(() => {
        carregaProdutos();
    }, []);

    async function carregaProdutos(){
        const response = await api.get("/produtos");
        setProdutos(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.name});
    }

    async function handleSalvar() {
        if(produtoSelecionado){
            await api.put("/produtos",  { ...form, idProduto: produtoSelecionado.idProduto});
        } else{
            await api.post("/produtos", form);
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
        await api.delete(`/produtos/${id}`);
    }

    return (
    <div>
      <h2>Gerenciar Produtos</h2>

      <div style={{ marginBottom: "20px" }}>
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
          <button onClick={() => { setProdutoSelecionado(null); setForm({ nomeProduto: "" }); }}>
            Cancelar
          </button>
        )}
      </div>

      <table border="1" cellPadding="8" style={{ width: "100%" }}>
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
              <td colSpan="4">Nenhum produto encontrado</td>
            </tr>
          ) : (
            produtos.map((p) => (
              <tr key={p.idProduto}>
                <td>{p.idProduto}</td>
                <td>{p.nomeProduto}</td>
                <td>
                  <button onClick={() => handleEditar(p)}>Editar</button>
                </td>
                <td>
                  <button onClick={() => handleExcluir(p.idProduto)}>Excluir</button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
    );
}

export default Produtos;