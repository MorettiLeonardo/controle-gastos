import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import * as pessoasService from "../services/pessoas";
import type { PessoaRequest } from "../types";

export default function PessoaForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const isEdicao = Boolean(id);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [loading, setLoading] = useState(false);
  const [carregando, setCarregando] = useState(isEdicao);

  useEffect(() => {
    handleObterPessoaPorId();
  }, [id]);

  const handleObterPessoaPorId = async () => {
    if (!id) return;
    try {
      const response = await pessoasService.obterPessoaPorId(id);
      if (response.data) {
        setNome(response.data.nome);
        setIdade(String(response.data.idade));
      }
    } finally {
      setCarregando(false);
    }
  };

  async function submit(e: React.FormEvent) {
    e.preventDefault();
    const idadeNum = parseInt(idade, 10);
    if (!nome.trim() || isNaN(idadeNum) || idadeNum < 0) {
      alert("Nome e idade válida são obrigatórios.");
      return;
    }
    if (nome.length > 200) {
      alert("Nome deve ter no máximo 200 caracteres.");
      return;
    }
    setLoading(true);
    const body: PessoaRequest = { nome: nome.trim(), idade: idadeNum };
    try {
      if (isEdicao) {
        const res = await pessoasService.atualizarPessoa(id!, body);
        if (res.data != null) {
          navigate("/");
          return;
        }
        alert(res.message || "Erro ao atualizar.");
      } else {
        const res = await pessoasService.criarPessoa(body);
        if (res.data != null) {
          navigate("/");
          return;
        }
        alert(res.message || "Erro ao cadastrar.");
      }
    } finally {
      setLoading(false);
    }
  }

  if (carregando) return <p className="text-slate-600">Carregando...</p>;

  return (
    <div className="bg-white rounded-lg border border-slate-200 shadow-sm p-6 max-w-md">
      <h1 className="text-xl font-semibold text-slate-800 mb-4">
        {isEdicao ? "Editar pessoa" : "Nova pessoa"}
      </h1>
      <form onSubmit={submit} className="flex flex-col gap-4">
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Nome</span>
          <input
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            maxLength={200}
            required
            className="mt-1 block w-full rounded border border-slate-300 px-3 py-2 text-slate-800 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
          />
        </label>
        <label className="block">
          <span className="text-slate-700 text-sm font-medium">Idade</span>
          <input
            type="number"
            min={0}
            value={idade}
            onChange={(e) => setIdade(e.target.value)}
            required
            className="mt-1 block w-full rounded border border-slate-300 px-3 py-2 text-slate-800 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500"
          />
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
            onClick={() => navigate("/")}
            className="px-4 py-2 bg-slate-200 text-slate-700 font-medium rounded hover:bg-slate-300"
          >
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
