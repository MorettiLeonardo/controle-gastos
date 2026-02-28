import { useState } from "react";
import { useNavigate } from "react-router-dom";
import * as categoriasService from "../services/categorias";
import type { CategoriaRequest } from "../types";
import { EFinalidade } from "../types";

export default function CategoriaForm() {
  const navigate = useNavigate();
  const [descricao, setDescricao] = useState("");
  const [finalidade, setFinalidade] = useState<EFinalidade>(EFinalidade.AMBAS);
  const [loading, setLoading] = useState(false);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    if (!descricao.trim()) {
      alert("Descrição é obrigatória.");
      return;
    }
    if (descricao.length > 400) {
      alert("Descrição deve ter no máximo 400 caracteres.");
      return;
    }
    setLoading(true);
    const body: CategoriaRequest = {
      descricao: descricao.trim(),
      finalidade: finalidade,
    };
    try {
      const res = await categoriasService.criarCategoria(body);
      if (res.data != null) {
        navigate("/categorias");
        return;
      }
      alert(res.message || "Erro ao cadastrar.");
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm p-6 max-w-md">
      <h1 className="text-xl font-semibold text-slate-800 mb-4">Nova categoria</h1>
      <form onSubmit={submit} className="flex flex-col gap-4">
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Descrição</span>
          <input
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            maxLength={400}
            required
            className="mt-1 block w-full rounded border border-slate-300 px-3 py-2 text-slate-800 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
          />
        </label>
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Finalidade</span>
          <select
            value={finalidade}
            onChange={(e) => setFinalidade(Number(e.target.value) as EFinalidade)}
            className="mt-1 block w-full rounded border border-slate-300 px-3 py-2 text-slate-800 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
          >
            <option value={EFinalidade.DESPESA}>Despesa</option>
            <option value={EFinalidade.RECEITA}>Receita</option>
            <option value={EFinalidade.AMBAS}>Ambas</option>
          </select>
        </label>
        <div className="flex gap-2">
          <button
            type="submit"
            disabled={loading}
            className="px-4 py-2 bg-blue-600 text-white font-medium rounded hover:bg-blue-700 disabled:opacity-50"
          >
            {loading ? "Salvando..." : "Salvar"}
          </button>
          <button
            type="button"
            onClick={() => navigate("/categorias")}
            className="px-4 py-2 bg-slate-200 text-slate-700 font-medium rounded hover:bg-slate-300"
          >
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
