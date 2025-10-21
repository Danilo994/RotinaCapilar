import { useEffect, useState } from "react";
import api from "../services/api";

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
    <div>
      <h2>Gerenciar Cuidados</h2>

      <div style={{ marginBottom: "20px" }}>
        <input
          type="datetime-local"
          name="dataCuidado"
          value={form.dataCuidado}
          onChange={handleChange}
        />
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
          style={{width: "100%", height: "120px"}}
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
            <th>Produtos</th>
            <th>Avaliação</th>
            <th>Editar</th>
            <th>Excluir</th>
          </tr>
        </thead>
        <tbody>
          {cuidados.length === 0 ? (
            <tr>
              <td colSpan="7">Nenhum cuidado encontrado</td>
            </tr>
          ) : (
            cuidados.map((c) => (
              <tr key={c.idCuidado}>
                <td>{c.idCuidado}</td>
                <td>{new Date(c.dataCuidado).toLocaleDateString()}</td>
                <td>{c.lavagem}</td>
                <td>{c.produtos.join(", ")}</td>
                <td>
                  {c.idAvaliacao ? (
                    <>
                      Nota: {c.nota} <br />
                      "{c.observacao}"
                      <button onClick={() => handleAvaliar(c.idCuidado)}>Editar</button>
                      <button onClick={() => handleExcluirAvaliacao(c.idCuidado)}>Excluir</button>
                    </>
                  ) : (
                    <button onClick={() => handleAvaliar(c.idCuidado)}>Avaliar</button>
                  )}
                </td>
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