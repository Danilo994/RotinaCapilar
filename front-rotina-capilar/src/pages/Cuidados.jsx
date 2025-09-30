import { useEffect, useState } from "react";
import api from "../services/api";

function Cuidados(){
    const [cuidados, setCuidados] = useState([]);
    const [cuidadoSelecionado, setCuidadoSelecionado] = useState(null);
    const [form, setForm] = useState({idCuidado: "0", dataCuidado: "", idLavagem: "0"});

    useEffect(() => {
        carregaCuidados();
    }, []);

    async function carregaCuidados(){
        const response = await api.get("/cuidados");
        setCuidados(response.data);
    }

    function handleChange(e){
        setForm({ ...form, [e.target.name]: e.target.name});
    }

    async function handleSalvar() {
        if(cuidadoSelecionado){
            await api.put("/cuidados", form)
        } else{
            await api.post("/cuidados", form);
        }
        setForm({idCuidado: 0, dataCuidado: "", idLavagem: 0});
        setCuidadoSelecionado(null);
        carregaCuidados();
    }

    function handleEditar(cuidado){
        setCuidadoSelecionado(cuidado);
        setForm(cuidado);
    }

    async function handleExcluir(id) {
        if(!window.confirm("Deseja excluir este cuidado?")) return;
        await api.delete(`/cuidados/${id}`);
    }

    return (
    <div>
      <h2>Gerenciar Cuidados</h2>

      <div style={{ marginBottom: "20px" }}>
        <input
          type="datetime-local"
          name="dataCuidado"
          value={form.dataCuidado}
          onChange={handleChange}
        />
        <input 
            type="number"
            name="idLavagem"
            value={form.idLavagem}
            onChange={handleChange}
            placeholder="Lavagem" 
        />
        <button onClick={handleSalvar}>
          {cuidadoSelecionado ? "Salvar Edição" : "Adicionar"}
        </button>
        {cuidadoSelecionado && (
          <button onClick={() => { setCuidadoSelecionado(null); setForm({idCuidado: 0, dataCuidado: "", idLavagem: 0}); }}>
            Cancelar
          </button>
        )}
      </div>

      <table border="1" cellPadding="8" style={{ width: "100%" }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Data do Cuidado</th>
            <th>Lavagem</th>
            <th>Editar</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {cuidados.length === 0 ? (
            <tr>
              <td colSpan="5">Nenhum cuidado encontrado</td>
            </tr>
          ) : (
            cuidados.map((c) => (
              <tr key={c.idCuidado}>
                <td>{c.idCuidado}</td>
                <td>{new Date(carregaCuidados.dataCuidado).toLocaleString()}</td>
                <td>{carregaCuidados.idLavagem}</td>
                <td>
                  <button onClick={() => handleEditar(c)}>Editar</button>
                </td>
                <td>
                  <button onClick={() => handleExcluir(c.idCuidado)}>Excluir</button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
    );
}

export default Cuidados;