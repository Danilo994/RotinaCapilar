import { useEffect, useState } from "react";
import api from "../services/api";
import Header from "../components/Header";
import "../styles/tables.css";

function Cuidados(){
    const [cuidados, setCuidados] = useState([]);
    const [lavagens, setLavagens] = useState([]);
    const [produtos, setProdutos] = useState([]);
    const [cuidadoSelecionado, setCuidadoSelecionado] = useState(null);
    const [produtosSelecionados, setProdutosSelecionados] = useState([]);
    const [form, setForm] = useState({idCuidado: "0", dataCuidado: "", idLavagem: "0"});

    useEffect(() => {
        carregaCuidados();
        carregaLavagens();
        carregaProdutos();
    }, []);

    async function carregaCuidados(){
        const response = await api.get("/Cuidado");
        const lista = response.data?.value || [];
        console.log(lista);
        setCuidados(lista);
    }

    async function carregaLavagens() {
      const response = await api.get("/Lavagem");
      setLavagens(response.data);
    }

    async function carregaProdutos() {
      const response = await api.get("/Produtos");
      setProdutos(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.value});
    }

    async function handleSalvar() {
      let idCuidado;
      if(cuidadoSelecionado){
          await api.put("/Cuidado", form);
          idCuidado = form.idCuidado;
      } else{
          const response = await api.post("/Cuidado", form);
          idCuidado = response.data.idCuidado;
      }

      if(produtosSelecionados.length > 0){
        if(cuidadoSelecionado){
          await api.delete(`/CuidadoProduto/cuidado/${idCuidado}`)
        }
        for(const idProduto of produtosSelecionados){
          await api.post("/CuidadoProduto", {
            idCuidado,
            idProduto: parseInt(idProduto)
          });
        }
      }

      setForm({idCuidado: 0, dataCuidado: "", idLavagem: 0});
      setCuidadoSelecionado(null);
      setProdutosSelecionados([]);
      carregaCuidados();
    }

    function handleEditar(cuidado){
        setCuidadoSelecionado(cuidado);
        setForm(cuidado);
    }

    async function handleExcluir(id) {
        if(!window.confirm("Deseja excluir este cuidado?")) return;
        await api.delete(`/Cuidado/${id}`);
        carregaCuidados();
    }

    const handleAvaliar = async (idCuidado) => {
      const nota = prompt("Digite a nota (1 a 5):");
      const observacao = prompt("Digite a observação:");
      await api.post("Cuidado/avaliar", {idCuidado, nota, observacao});
      alert("Avaliação salva");
      carregaCuidados();
    }

    async function handleExcluirAvaliacao(idCuidado) {
      if(!window.confirm("Deseja excluir essa avaliação?")) return;
      await api.delete(`/Cuidado/avaliar/${idCuidado}`);
      carregaCuidados();
    }

    return (
      <>
      <Header />
    <div className="page-container">
      <h2 className="page-title">Gerenciar Cuidados</h2>

      <div className="form-container cuidados-form">
        <label>Data do Cuidado:</label>
        <input
          type="datetime-local"
          name="dataCuidado"
          value={form.dataCuidado}
          onChange={handleChange}
        />
        <label>Tipo de Lavagem:</label>
        <select 
            name="idLavagem"
            value={form.idLavagem}
            onChange={handleChange}
        >
          <option value="">Selecione uma lavagem</option>
          {lavagens.map((lav) => (
            <option key={lav.idLavagem} value={lav.idLavagem}>
              {lav.nomeLavagem}
            </option>
          ))}
        </select>
        <label>Produtos usados:</label>
        <select
          multiple
          name="produtos"
          value={produtosSelecionados}
          onChange={(e) => {
            const valores = Array.from(e.target.selectedOptions, (opt) => opt.value);
            setProdutosSelecionados(valores);
          }}
          className="multiselect"
        >
          {produtos.map((p) => (
            <option key={p.idProduto} value={p.idProduto}>
              {p.nomeProduto}
            </option>
          ))}
        </select>
        <button onClick={handleSalvar}>
          {cuidadoSelecionado ? "Salvar Edição" : "Adicionar"}
        </button>
        {cuidadoSelecionado && (
          <button className="cancelar" onClick={() => { setCuidadoSelecionado(null); setForm({idCuidado: 0, dataCuidado: "", idLavagem: 0}); }}>
            Cancelar
          </button>
        )}
      </div>

      <table className="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Data do Cuidado</th>
            <th>Lavagem</th>
            <th>Produtos</th>
            <th>Avaliação</th>
            <th>Editar</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {cuidados.length === 0 ? (
            <tr>
              <td colSpan="7" className="empty-row">Nenhum cuidado encontrado</td>
            </tr>
          ) : (
            cuidados.map((c) => (
              <tr key={c.idCuidado}>
                <td>{c.idCuidado}</td>
                <td>{new Date(c.dataCuidado).toLocaleDateString()}</td>
                <td>{c.lavagem}</td>
                <td>{c.produtos.join(", ")}</td>
                <td className="avaliacao-cell">
                  {c.idAvaliacao ? (
                    <div className="avaliacao">
                      <p><strong>Nota:</strong> {c.nota}</p>
                      <p className="observacao">"{c.observacao}"</p>
                      <div className="avaliacao-buttons">
                        <button className="edit" onClick={() => handleAvaliar(c.idCuidado)}>Editar</button>
                        <button className="delete" onClick={() => handleExcluirAvaliacao(c.idCuidado)}>Excluir</button>
                      </div>
                    </div>
                  ) : (
                    <button className="avaliar-btn" onClick={() => handleAvaliar(c.idCuidado)}>Avaliar</button>
                  )}
                </td>
                <td>
                  <button className="edit" onClick={() => handleEditar(c)}>Editar</button>
                </td>
                <td>
                  <button className="delete" onClick={() => handleExcluir(c.idCuidado)}>Excluir</button>
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

export default Cuidados;