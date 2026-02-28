import api from "../config/api";
import type {
  ApiResponse,
  CategoriaRequest,
  CategoriaResponse,
  ObterTodasCategoriasResponse,
  ConsultaTotaisCategoriaResponse,
} from "../types";

export async function criarCategoria(
  body: CategoriaRequest
): Promise<ApiResponse<CategoriaResponse>> {
  const { data } = await api.post<ApiResponse<CategoriaResponse>>(
    "/categoria",
    body
  );
  return data;
}

export async function obterCategorias(
  pagina: number = 1,
  tamanhoPagina: number = 100
): Promise<ApiResponse<ObterTodasCategoriasResponse[]>> {
  const { data } = await api.get<
    ApiResponse<ObterTodasCategoriasResponse[]>
  >("/categoria", { params: { pagina, tamanhoPagina } });
  return data;
}

export async function consultarTotaisPorCategoria(): Promise<
  ApiResponse<ConsultaTotaisCategoriaResponse>
> {
  const { data } = await api.get<ApiResponse<ConsultaTotaisCategoriaResponse>>(
    "/categoria/total"
  );
  return data;
}
