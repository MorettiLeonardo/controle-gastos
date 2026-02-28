import { useEffect, useState } from "react";
import * as pessoasService from "../services/pessoas";
import type { ConsultaTotaisPessoasResponse } from "../types";

function formatarValor(v: number): string {
  return v.toLocaleString("pt-BR", {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });
}

export default function TotaisPessoa() {
  const [data, setData] = useState<ConsultaTotaisPessoasResponse | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    handleTotaisPessoas();
  }, []);

  const handleTotaisPessoas = async () => {
    try {
      const response = await pessoasService.obterPessoas();
      if (response.data) setData(response.data);
      else setData(null);
    } catch {
      alert("Erro ao carregar totais.");
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <p className="text-slate-600">Carregando...</p>;
  if (!data) return <p className="text-slate-600">Nenhum dado disponível.</p>;

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm overflow-hidden">
      <div className="px-4 py-3 border-b border-slate-200">
        <h1 className="text-xl font-semibold text-slate-800">Totais por Pessoa</h1>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b-2 border-slate-300 bg-slate-50">
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Pessoa</th>
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Total Receitas</th>
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Total Despesas</th>
              <th className="text-left py-3 px-4 text-slate-600 font-medium">Saldo</th>
            </tr>
          </thead>
          <tbody>
            {data.pessoas.map((p) => (
              <tr key={p.pessoaId} className="border-b border-slate-100 hover:bg-slate-50">
                <td className="py-3 px-4 text-slate-800">{p.nome}</td>
                <td className="py-3 px-4 text-slate-700">R$ {formatarValor(p.totalReceitas)}</td>
                <td className="py-3 px-4 text-slate-700">R$ {formatarValor(p.totalDespesas)}</td>
                <td className="py-3 px-4 text-slate-700">R$ {formatarValor(p.saldo)}</td>
              </tr>
            ))}
          </tbody>
          <tfoot>
            <tr className="border-t-2 border-slate-300 bg-slate-100 font-semibold">
              <td className="py-3 px-4 text-slate-800">Total geral</td>
              <td className="py-3 px-4 text-slate-800">R$ {formatarValor(data.totalGeralReceitas)}</td>
              <td className="py-3 px-4 text-slate-800">R$ {formatarValor(data.totalGeralDespesas)}</td>
              <td className="py-3 px-4 text-slate-800">R$ {formatarValor(data.saldoLiquido)}</td>
            </tr>
          </tfoot>
        </table>
      </div>
      {data.pessoas.length === 0 && (
        <p className="py-6 px-4 text-slate-500 text-center">Nenhuma pessoa cadastrada.</p>
      )}
    </div>
  );
}
