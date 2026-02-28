import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as categoriasService from "../services/categorias";
import type { ObterTodasCategoriasResponse } from "../types";
import { EFinalidade } from "../types";

const finalidadeLabel: Record<EFinalidade, string> = {
  [EFinalidade.DESPESA]: "Despesa",
  [EFinalidade.RECEITA]: "Receita",
  [EFinalidade.AMBAS]: "Ambas",
};

export default function Categorias() {
  const [lista, setLista] = useState<ObterTodasCategoriasResponse[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    handleTodasCategorias();
  }, []);

  const handleTodasCategorias = async () => {
    try {
      const response = await categoriasService.obterCategorias(1, 500);
      if (response.data && Array.isArray(response.data)) setLista(response.data);
      else setLista([]);
    } catch {
      alert("Erro ao carregar categorias.");
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <p className="text-slate-600">Carregando...</p>;

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm overflow-hidden">
      <div className="px-4 py-3 border-b border-slate-200 flex justify-between items-center">
        <h1 className="text-xl font-semibold text-slate-800">Categorias</h1>
        <Link
          to="/categorias/nova"
          className="px-3 py-1.5 bg-blue-600 text-white text-sm font-medium rounded hover:bg-blue-700"
        >
          Nova categoria
        </Link>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b border-slate-200 bg-slate-50">
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Descrição</th>
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Finalidade</th>
            </tr>
          </thead>
          <tbody>
            {lista.map((c) => (
              <tr key={c.id} className="border-b border-slate-100 hover:bg-slate-50">
                <td className="py-3 px-4 text-slate-800">{c.descricao}</td>
                <td className="py-3 px-4 text-slate-700">{finalidadeLabel[c.finalidade] ?? c.finalidade}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {lista.length === 0 && (
        <p className="py-6 px-4 text-slate-500 text-center">Nenhuma categoria cadastrada.</p>
      )}
    </div>
  );
}
