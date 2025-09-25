import { useEffect, useState } from "react";
import api from "../services/api";

function Lavagem(){
    const [lavagens, setLavagens] = useState([]);
    const [lavagemSelecionada, setLavagemSelecionada] = useState(null);
    const [form, setForm] = useState({nomeLavagem: ""});

    useEffect(() => {
        carregaLavagens();
    }, []);

    async function carregaLavagens(){
        const response = await api.get("/lavagem");
        setLavagens(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.name});
    }

    async function handleSalvar() {
        if(lavagemSelecionada){
            await api.put("/lavagem",  { ...form, idLavagem: lavagemSelecionada.idLavagem});
        } else{
            await api.post("/lavagem", form);
        }
        setForm({nomeLavagem: ""});
        setLavagemSelecionada(null);
        carregaLavagens();
    }

    function handleEditar(lavagem){
        setLavagemSelecionada(lavagem);
        setForm({nomeLavagem: lavagem.nomeLavagem});
    }

    async function handleExcluir(id) {
        if(!window.confirm("Deseja excluir esta lavagem?")) return;
        await api.delete(`/lavagem/${id}`);
    }

    return (
    <div>
      <h2>Gerenciar Lavagens</h2>

      <div style={{ marginBottom: "20px" }}>
        <input
          type="text"
          name="nomeLavagem"
          value={form.nomeLavagem}
          onChange={handleChange}
          placeholder="Nome da Lavagem"
        />
        <button onClick={handleSalvar}>
          {lavagemSelecionada ? "Salvar Edição" : "Adicionar"}
        </button>
        {lavagemSelecionada && (
          <button onClick={() => { setLavagemSelecionada(null); setForm({ nomeLavagem: "" }); }}>
            Cancelar
          </button>
        )}
      </div>

      <table border="1" cellPadding="8" style={{ width: "100%" }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome Lavagem</th>
            <th>Editar</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {lavagens.length === 0 ? (
            <tr>
              <td colSpan="4">Nenhuma lavagem encontrado</td>
            </tr>
          ) : (
            lavagens.map((p) => (
              <tr key={p.idLavagem}>
                <td>{p.idLavagem}</td>
                <td>{p.nomeLavagem}</td>
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

export default Lavagem;