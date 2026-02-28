import api from "../config/api";
import type {
  ApiResponse,
  PessoaRequest,
  PessoaResponse,
  ConsultaTotaisPessoasResponse,
} from "../types";

export async function criarPessoa(
  body: PessoaRequest
): Promise<ApiResponse<PessoaResponse>> {
  const { data } = await api.post<ApiResponse<PessoaResponse>>("/pessoas", body);
  return data;
}

export async function obterPessoas(): Promise<
  ApiResponse<ConsultaTotaisPessoasResponse>
> {
  const { data } = await api.get<ApiResponse<ConsultaTotaisPessoasResponse>>(
    "/pessoas"
  );
  return data;
}

export async function obterPessoaPorId(
  pessoaId: string
): Promise<ApiResponse<PessoaResponse>> {
  const { data } = await api.get<ApiResponse<PessoaResponse>>(
    `/pessoas/${pessoaId}`
  );
  return data;
}

export async function atualizarPessoa(
  pessoaId: string,
  body: PessoaRequest
): Promise<ApiResponse<PessoaResponse>> {
  const { data } = await api.put<ApiResponse<PessoaResponse>>(
    `/pessoas/${pessoaId}`,
    body
  );
  return data;
}

export async function deletarPessoa(
  pessoaId: string
): Promise<ApiResponse<null>> {
  const { data } = await api.delete<ApiResponse<null>>(
    `/pessoas/${pessoaId}`
  );
  return data;
}
