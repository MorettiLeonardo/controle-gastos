import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/Layout";
import Pessoas from "./pages/Pessoas";
import PessoaForm from "./pages/PessoaForm";
import Categorias from "./pages/Categorias";
import CategoriaForm from "./pages/CategoriaForm";
import Transacoes from "./pages/Transacoes";
import TransacaoForm from "./pages/TransacaoForm";
import TotaisPessoa from "./pages/TotaisPessoa";
import TotaisCategoria from "./pages/TotaisCategoria";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Pessoas />} />
          <Route path="pessoas/nova" element={<PessoaForm />} />
          <Route path="pessoas/editar/:id" element={<PessoaForm />} />
          <Route path="categorias" element={<Categorias />} />
          <Route path="categorias/nova" element={<CategoriaForm />} />
          <Route path="transacoes" element={<Transacoes />} />
          <Route path="transacoes/nova" element={<TransacaoForm />} />
          <Route path="totais-pessoa" element={<TotaisPessoa />} />
          <Route path="totais-categoria" element={<TotaisCategoria />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
