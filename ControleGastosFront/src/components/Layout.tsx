import { Link, Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <div className="min-h-screen bg-slate-50 font-sans">
      <nav className="bg-white border-b border-slate-200 px-4 py-3">
        <div className="max-w-4xl mx-auto flex flex-wrap gap-4">
          <Link
            to="/"
            className="text-slate-600 hover:text-slate-900 font-medium"
          >
            Pessoas
          </Link>
          <Link
            to="/categorias"
            className="text-slate-600 hover:text-slate-900 font-medium"
          >
            Categorias
          </Link>
          <Link
            to="/transacoes"
            className="text-slate-600 hover:text-slate-900 font-medium"
          >
            Transações
          </Link>
          <Link
            to="/totais-pessoa"
            className="text-slate-600 hover:text-slate-900 font-medium"
          >
            Totais por Pessoa
          </Link>
          <Link
            to="/totais-categoria"
            className="text-slate-600 hover:text-slate-900 font-medium"
          >
            Totais por Categoria
          </Link>
        </div>
      </nav>
      <main className="max-w-4xl mx-auto p-4">
        <Outlet />
      </main>
    </div>
  );
}
