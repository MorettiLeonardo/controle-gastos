import api from "../config/api";
import type {
  ApiResponse,
  TransacaoRequest,
  Transacao,
} from "../types";

export async function criarTransacao(
  body: TransacaoRequest
): Promise<ApiResponse<unknown>> {
  const { data } = await api.post<ApiResponse<unknown>>("/transacao", body);
  return data;
}

export async function obterTransacoesPorPessoa(
  pessoaId: string,
  pagina: number = 1,
  tamanhoPagina: number = 100
): Promise<ApiResponse<Transacao[]>> {
  const { data } = await api.get<ApiResponse<Transacao[]>>(
    `/transacao/pessoa/${pessoaId}`,
    { params: { pagina, tamanhoPagina } }
  );
  return data;
}
