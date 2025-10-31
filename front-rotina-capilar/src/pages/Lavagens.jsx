import { useEffect, useState } from "react";
import api from "../services/api";
import Header from "../components/Header";
import "../styles/tables.css";

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

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({
          ...prev,
          [name]: value
        }));
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
        carregaLavagens();
    }

    return (
      <>
      <Header />
    <div className="page-container">
      <h2 className="page-title">Gerenciar Lavagens</h2>

      <div className="form-container">
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
          <button className="cancelar" onClick={() => { setLavagemSelecionada(null); setForm({ nomeLavagem: "" }); }}>
            Cancelar
          </button>
        )}
      </div>

      <table className="table">
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
              <td colSpan="4" className="empty-row">Nenhuma lavagem encontrado</td>
            </tr>
          ) : (
            lavagens.map((l) => (
              <tr key={l.idLavagem}>
                <td>{l.idLavagem}</td>
                <td>{l.nomeLavagem}</td>
                <td>
                  <button className="edit" onClick={() => handleEditar(l)}>Editar</button>
                </td>
                <td>
                  <button className="delete" onClick={() => handleExcluir(l.idLavagem)}>Excluir</button>
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