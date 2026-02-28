import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as pessoasService from "../services/pessoas";
import * as transacoesService from "../services/transacoes";
import type { PessoaTotaisResponse, Transacao } from "../types";

const tipoLabel: Record<string, string> = {
  DESPESA: "Despesa",
  RECEITA: "Receita",
};

export default function Transacoes() {
  const [pessoas, setPessoas] = useState<PessoaTotaisResponse[]>([]);
  const [pessoaId, setPessoaId] = useState("");
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [loading, setLoading] = useState(true);
  const [loadingTransacoes, setLoadingTransacoes] = useState(false);
  const [erro, setErro] = useState("");

  useEffect(() => {
    handleObterPessoas();
  }, []);

  useEffect(() => {
    handleTransacoesPorPessoa();
  }, [pessoaId]);

  const handleObterPessoas = async () => {
    try {
      const response = await pessoasService.obterPessoas();
      if (response.data?.pessoas) setPessoas(response.data.pessoas);
      else setPessoas([]);
    } catch {
      setErro("Erro ao carregar pessoas.");
    } finally {
      setLoading(false);
    }
  };

  const handleTransacoesPorPessoa = async () => {
    if (!pessoaId) {
      setTransacoes([]);
      return;
    }
    setLoadingTransacoes(true);
    try {
      const response = await transacoesService.obterTransacoesPorPessoa(pessoaId);
      if (response.data && Array.isArray(response.data)) setTransacoes(response.data);
      else setTransacoes([]);
    } catch {
      setTransacoes([]);
    } finally {
      setLoadingTransacoes(false);
    }
  };

  if (loading) return <p className="text-slate-600">Carregando...</p>;
  if (erro) return <p className="text-red-600">{erro}</p>;

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm overflow-hidden">
      <div className="px-4 py-3 border-b border-slate-200 flex justify-between items-center flex-wrap gap-2">
        <h1 className="text-xl font-semibold text-slate-800">Transações</h1>
        <Link
          to="/transacoes/nova"
          className="px-3 py-1.5 bg-blue-600 text-white text-sm font-medium rounded hover:bg-blue-700"
        >
          Nova transação
        </Link>
      </div>
      <div className="p-4 border-b border-slate-100">
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Pessoa</span>
          <select
            value={pessoaId}
            onChange={(e) => setPessoaId(e.target.value)}
            className="mt-1 block w-full max-w-xs rounded border border-slate-300 px-3 py-2 text-slate-800 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
          >
            <option value="">Selecione uma pessoa</option>
            {pessoas.map((p) => (
              <option key={p.pessoaId} value={p.pessoaId}>
                {p.nome}
              </option>
            ))}
          </select>
        </label>
      </div>
      {pessoaId && (
        <>
          {loadingTransacoes ? (
            <p className="py-6 px-4 text-slate-600">Carregando transações...</p>
          ) : (
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead>
                  <tr className="border-b border-slate-200 bg-slate-50">
                    <th className="text-left py-3 px-4 text-slate-600 font-medium">Descrição</th>
                    <th className="text-left py-3 px-4 text-slate-600 font-medium">Valor</th>
                    <th className="text-left py-3 px-4 text-slate-600 font-medium">Tipo</th>
                  </tr>
                </thead>
                <tbody>
                  {transacoes.map((t) => (
                    <tr key={t.id} className="border-b border-slate-100 hover:bg-slate-50">
                      <td className="py-3 px-4 text-slate-800">{t.descricao}</td>
                      <td className="py-3 px-4 text-slate-700">{Number(t.valor).toFixed(2)}</td>
                      <td className="py-3 px-4 text-slate-700">{tipoLabel[t.tipo] ?? t.tipo}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
          {!loadingTransacoes && transacoes.length === 0 && (
            <p className="py-6 px-4 text-slate-500 text-center">Nenhuma transação para esta pessoa.</p>
          )}
        </>
      )}
    </div>
  );
}
