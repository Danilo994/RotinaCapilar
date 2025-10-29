import { useEffect, useState } from "react";
import api from "../services/api";
import Header from "../components/Header";

function Lavagens(){
    const [lavagens, setLavagens] = useState([]);
    const [lavagemSelecionada, setLavagemSelecionada] = useState(null);
    const [form, setForm] = useState({nomeLavagem: ""});

    useEffect(() => {
        carregaLavagens();
    }, []);

    async function carregaLavagens(){
        const response = await api.get("/Lavagem");
        setLavagens(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.name});
    }

    async function handleSalvar() {
        if(lavagemSelecionada){
            await api.put("/Lavagem",  { ...form, idLavagem: lavagemSelecionada.idLavagem});
        } else{
            await api.post("/Lavagem", form);
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
        await api.delete(`/Lavagem/${id}`);
    }

    return (
      <>
      <Header />
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
            lavagens.map((l) => (
              <tr key={l.idLavagem}>
                <td>{l.idLavagem}</td>
                <td>{l.nomeLavagem}</td>
                <td>
                  <button onClick={() => handleEditar(l)}>Editar</button>
                </td>
                <td>
                  <button onClick={() => handleExcluir(l.idProduto)}>Excluir</button>
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

export default Lavagens;