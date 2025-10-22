import Header from "../components/Header";
import "../styles/global.css";
import produtosImg from "../assets/produtos.jpg";
import lavagemImg from "../assets/lavagem.png";
import cuidadoImg from "../assets/cuidado.jpg";

function Home() {
  return (
    <>
      <Header />
      <main className="container">
        <section className="intro">
          <h1>Bem-vindo à Rotina Capilar</h1>
          <p>
            Aqui você pode registrar e acompanhar os cuidados capilares que realiza no dia a dia.
            Descubra a rotina que mais combina com você e mantenha seu cabelo sempre saudável e bonito.
          </p>
        </section>

        <section className="content">
          <div className="card">
            <img src={produtosImg} alt="prateleiras com produtos de cabelo"/>
            <p>
                Cadastre os produtos que você utiliza e experimente diferentes combinações para descobrir o que traz os melhores resultados ao seu cabelo.
            </p>
          </div>
          <div className="card reverse">
            <img src={lavagemImg} alt="pessoal lavando cabelo"/>
            <p>
                Registre os tipos de lavagem que você realiza e mantenha um histórico completo para acompanhar a frequência e os resultados de cada uma delas.
            </p>
          </div>
          <div className="card">
            <img src={cuidadoImg} alt="tabela de tipos de cabelo"/>
            <p>
                Com todas as informações registradas, você pode avaliar seus cuidados e acompanhar a evolução do seu cabelo ao longo do tempo — garantindo mais confiança na sua rotina capilar ideal.
            </p>
          </div>
        </section>
      </main>

      <footer>
        <p>© 2025 Rotina Capilar - Todos os direitos reservados</p>
      </footer>
    </>
  );
}

export default Home;
