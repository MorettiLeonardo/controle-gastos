import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import * as pessoasService from "../services/pessoas";
import * as categoriasService from "../services/categorias";
import * as transacoesService from "../services/transacoes";
import type { PessoaTotaisResponse, ObterTodasCategoriasResponse } from "../types";
import { EFinalidade, ETipo } from "../types";

const FINALIDADE_STRINGS = ["DESPESA", "RECEITA", "AMBAS"] as const;
type FinalidadeStr = (typeof FINALIDADE_STRINGS)[number];

function finalidadeComoString(fin: EFinalidade | string | number): FinalidadeStr {
  if (typeof fin === "string" && FINALIDADE_STRINGS.includes(fin as FinalidadeStr)) return fin as FinalidadeStr;
  if (typeof fin === "number" && fin >= 0 && fin <= 2) return FINALIDADE_STRINGS[fin];
  return "AMBAS";
}

function labelFinalidade(fin: EFinalidade | string | number): string {
  const f = finalidadeComoString(fin);
  return f === "AMBAS" ? "Ambas" : f === "DESPESA" ? "Despesa" : "Receita";
}

function categoriaServeParaTipo(finalidade: EFinalidade | string | number, tipo: ETipo): boolean {
  const f = finalidadeComoString(finalidade);
  if (f === "AMBAS") return true;
  return tipo === ETipo.DESPESA ? f === "DESPESA" : f === "RECEITA";
}

export default function TransacaoForm() {
  const navigate = useNavigate();
  const [pessoas, setPessoas] = useState<PessoaTotaisResponse[]>([]);
  const [categorias, setCategorias] = useState<ObterTodasCategoriasResponse[]>([]);
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState<ETipo>(ETipo.DESPESA);
  const [pessoaId, setPessoaId] = useState("");
  const [categoriaId, setCategoriaId] = useState("");
  const [loading, setLoading] = useState(false);
  const [carregando, setCarregando] = useState(true);
  const [idadePessoa, setIdadePessoa] = useState<number | null>(null);

  useEffect(() => {
    handleCarregarPessoasECategorias();
  }, []);

  useEffect(() => {
    handleObterIdadePessoa();
  }, [pessoaId]);

  useEffect(() => {
    handleAjustarTipoMenorIdade();
  }, [pessoaId, idadePessoa, tipo]);

  const handleCarregarPessoasECategorias = async () => {
    try {
      const [resP, resC] = await Promise.all([
        pessoasService.obterPessoas(),
        categoriasService.obterCategorias(1, 500),
      ]);
      if (resP.data?.pessoas) setPessoas(resP.data.pessoas);
      if (resC.data && Array.isArray(resC.data)) setCategorias(resC.data);
    } finally {
      setCarregando(false);
    }
  };

  const handleObterIdadePessoa = async () => {
    if (!pessoaId) {
      setIdadePessoa(null);
      return;
    }
    const response = await pessoasService.obterPessoaPorId(pessoaId);
    setIdadePessoa(response.data ? response.data.idade : null);
  };

  const handleAjustarTipoMenorIdade = () => {
    if (idadePessoa !== null && idadePessoa < 18 && tipo === ETipo.RECEITA) {
      setTipo(ETipo.DESPESA);
    }
  };

  const categoriasFiltradas = categorias.filter((c) => categoriaServeParaTipo(c.finalidade, tipo));
  const precisaApenasDespesa = idadePessoa !== null && idadePessoa < 18;
  const categoriaSelecionadaValida = categoriasFiltradas.some((c) => c.id === categoriaId);

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    const valorNum = parseFloat(valor.replace(",", "."));
    if (!descricao.trim()) {
      alert("Descrição é obrigatória.");
      return;
    }
    if (descricao.length > 400) {
      alert("Descrição deve ter no máximo 400 caracteres.");
      return;
    }
    if (isNaN(valorNum) || valorNum <= 0) {
      alert("Valor deve ser um número positivo.");
      return;
    }
    if (!pessoaId || !categoriaId) {
      alert("Selecione pessoa e categoria.");
      return;
    }
    if (precisaApenasDespesa && tipo === ETipo.RECEITA) {
      alert("Menores de 18 anos só podem registrar despesas.");
      return;
    }
    setLoading(true);
    try {
      const res = await transacoesService.criarTransacao({
        descricao: descricao.trim(),
        valor: valorNum,
        tipo,
        pessoaId,
        categoriaId,
      });
      if (res.data !== undefined || res.message?.toLowerCase().includes("sucesso")) {
        navigate("/transacoes");
        return;
      }
      alert(res.message || "Erro ao cadastrar.");
    } catch {
      alert("Erro ao cadastrar.");
    } finally {
      setLoading(false);
    }
  }

  if (carregando) return <p className="text-slate-600">Carregando...</p>;

  const inputClass =
    "mt-1 block w-full rounded border border-slate-300 px-3 py-2 text-slate-800 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500";

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm p-6 max-w-md">
      <h1 className="text-xl font-semibold text-slate-800 mb-4">Nova transação</h1>
      <form onSubmit={submit} className="flex flex-col gap-4">
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Pessoa</span>
          <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} required className={inputClass}>
            <option value="">Selecione</option>
            {pessoas.map((p) => (
              <option key={p.pessoaId} value={p.pessoaId}>
                {p.nome}
              </option>
            ))}
          </select>
        </label>
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Descrição</span>
          <input
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            maxLength={400}
            required
            className={inputClass}
          />
        </label>
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Valor (positivo)</span>
          <input
            type="text"
            inputMode="decimal"
            value={valor}
            onChange={(e) => setValor(e.target.value)}
            required
            className={inputClass}
          />
        </label>
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Tipo</span>
          <select
            value={tipo}
            onChange={(e) => setTipo(e.target.value as ETipo)}
            disabled={precisaApenasDespesa}
            className={`${inputClass} disabled:bg-slate-100`}
          >
            <option value={ETipo.DESPESA}>Despesa</option>
            <option value={ETipo.RECEITA}>Receita</option>
          </select>
          {precisaApenasDespesa && (
            <span className="text-xs text-slate-500 mt-1 block">Menor de 18 anos: apenas despesas.</span>
          )}
        </label>
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Categoria</span>
          <select
            value={categoriaSelecionadaValida ? categoriaId : ""}
            onChange={(e) => setCategoriaId(e.target.value)}
            required
            className={inputClass}
          >
            <option value="">Selecione</option>
            {categoriasFiltradas.map((c) => (
              <option key={c.id} value={c.id}>
                {c.descricao} ({labelFinalidade(c.finalidade)})
              </option>
            ))}
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
            onClick={() => navigate("/transacoes")}
            className="px-4 py-2 bg-slate-200 text-slate-700 font-medium rounded hover:bg-slate-300"
          >
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
