import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as pessoasService from "../services/pessoas";
import type { PessoaTotaisResponse } from "../types";

export default function Pessoas() {
  const [lista, setLista] = useState<PessoaTotaisResponse[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    handleObterPessoas();
  }, []);

  const handleObterPessoas = async () => {
    try {
      const response = await pessoasService.obterPessoas();
      if (response.data?.pessoas) setLista(response.data.pessoas);
      else setLista([]);
    } catch {
      alert("Erro ao carregar pessoas.");
    } finally {
      setLoading(false);
    }
  };

  async function excluir(id: string) {
    if (!confirm("Excluir esta pessoa? Todas as transações serão apagadas.")) return;
    try {
      await pessoasService.deletarPessoa(id);
      setLista((prev) => prev.filter((p) => p.pessoaId !== id));
    } catch {
      alert("Erro ao excluir.");
    }
  }

  if (loading) return <p className="text-slate-600">Carregando...</p>;

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm overflow-hidden">
      <div className="px-4 py-3 border-b border-slate-200 flex justify-between items-center">
        <h1 className="text-xl font-semibold text-slate-800">Pessoas</h1>
        <Link
          to="/pessoas/nova"
          className="px-3 py-1.5 bg-blue-600 text-white text-sm font-medium rounded hover:bg-blue-700"
        >
          Nova pessoa
        </Link>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b border-slate-200 bg-slate-50">
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Nome</th>
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Ações</th>
            </tr>
          </thead>
          <tbody>
            {lista.map((p) => (
              <tr key={p.pessoaId} className="border-b border-slate-100 hover:bg-slate-50">
                <td className="py-3 px-4 text-slate-800">{p.nome}</td>
                <td className="py-3 px-4">
                  <Link
                    to={`/pessoas/editar/${p.pessoaId}`}
                    className="text-blue-600 hover:underline mr-3"
                  >
                    Editar
                  </Link>
                  <button
                    type="button"
                    onClick={() => excluir(p.pessoaId)}
                    className="text-red-600 hover:underline"
                  >
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {lista.length === 0 && (
        <p className="py-6 px-4 text-slate-500 text-center">Nenhuma pessoa cadastrada.</p>
      )}
    </div>
  );
}
