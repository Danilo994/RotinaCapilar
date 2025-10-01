import { useEffect, useState } from "react";
import api from "../services/api";

function Avaliacoes(){
    const [avaliacoes, setAvaliacoes] = useState([]);
    const [avaliacaoSelecionada, setAvaliacaoSelecionada] = useState(null);
    const [form, setForm] = useState({idAvaliacao: "0", nota: "", observacao: "", dataAvaliacao: "", idCuidado: "0"});

    useEffect(() => {
        carregaAvaliacoes();
    }, []);

    async function carregaAvaliacoes(){
        const response = await api.get("/Avaliacao");
        setAvaliacoes(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.name});
    }

    async function handleSalvar() {
        if(avaliacaoSelecionada){
            await api.put("/Avaliacao", form)
        } else{
            await api.post("/Avaliacao", form);
        }
        setForm({idAvaliacao: 0, nota: "", observacao: "", dataAvaliacao: "", idCuidado: 0});
        setAvaliacaoSelecionada(null);
        carregaAvaliacoes();
    }

    function handleEditar(avaliacao){
        setAvaliacaoSelecionada(avaliacao);
        setForm(avaliacao);
    }

    async function handleExcluir(id) {
        if(!window.confirm("Deseja excluir esta avaliação?")) return;
        await api.delete(`/Avaliacao/${id}`);
    }

    return (
    <div>
      <h2>Gerenciar Avaliações</h2>

      <div style={{ marginBottom: "20px" }}>
        <input
          type="number"
          name="nota"
          value={form.nota}
          onChange={handleChange}
          placeholder="Nota" 
        />
        <input
          type="string"
          name="observacao"
          value={form.observacao}
          onChange={handleChange}
          placeholder="Observação" 
        />
        <input
          type="datetime-local"
          name="dataAvaliacao"
          value={form.dataAvaliacao}
          onChange={handleChange}
        />
        <input 
            type="number"
            name="idCuidado"
            value={form.idCuidado}
            onChange={handleChange}
            placeholder="Cuidado" 
        />
        <button onClick={handleSalvar}>
          {avaliacaoSelecionada ? "Salvar Edição" : "Adicionar"}
        </button>
        {avaliacaoSelecionada && (
          <button onClick={() => { setAvaliacaoSelecionada(null); setForm({idAvaliacao: 0, nota: "", observacao: "", dataAvaliacao: "", idCuidado: 0}); }}>
            Cancelar
          </button>
        )}
      </div>

      <table border="1" cellPadding="8" style={{ width: "100%" }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nota</th>
            <th>Observação</th>
            <th>Data da Avaliação</th>
            <th>Cuidado</th>
            <th>Editar</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {avaliacoes.length === 0 ? (
            <tr>
              <td colSpan="7">Nenhuma avaliação encontrada</td>
            </tr>
          ) : (
            avaliacoes.map((a) => (
              <tr key={a.idAvaliacao}>
                <td>{a.idAvaliacao}</td>
                <td>{a.nota}</td>
                <td>{a.observacao}</td>
                <td>{new Date(carregaAvaliacoes.dataAvaliacao).toLocaleString()}</td>
                <td>{carregaAvaliacoes.idCuidado}</td>
                <td>
                  <button onClick={() => handleEditar(a)}>Editar</button>
                </td>
                <td>
                  <button onClick={() => handleExcluir(a.idAvaliacao)}>Excluir</button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
    );
}

export default Avaliacoes;