export const EFinalidade = {
  DESPESA: 0,
  RECEITA: 1,
  AMBAS: 2,
} as const;
export type EFinalidade = (typeof EFinalidade)[keyof typeof EFinalidade];

export const ETipo = {
  DESPESA: "DESPESA",
  RECEITA: "RECEITA",
} as const;
export type ETipo = (typeof ETipo)[keyof typeof ETipo];

export interface ApiResponse<T> {
  message: string;
  data: T | null;
}

export interface PessoaRequest {
  nome: string;
  idade: number;
}

export interface PessoaResponse {
  nome: string;
  idade: number;
}

export interface PessoaTotaisResponse {
  pessoaId: string;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface ConsultaTotaisPessoasResponse {
  pessoas: PessoaTotaisResponse[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoLiquido: number;
}

export interface CategoriaRequest {
  descricao: string;
  finalidade: EFinalidade;
}

export interface CategoriaResponse {
  descricao: string;
  finalidade: EFinalidade;
}

export interface ObterTodasCategoriasResponse {
  id: string;
  descricao: string;
  finalidade: EFinalidade;
}

export interface CategoriasTotaisResponse {
  categoriaId: string;
  descricao: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface ConsultaTotaisCategoriaResponse {
  categorias: CategoriasTotaisResponse[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoLiquido: number;
}

export interface TransacaoRequest {
  descricao: string;
  valor: number;
  tipo: ETipo;
  pessoaId: string;
  categoriaId: string;
}

export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: string;
  categoriaId: string;
  pessoaId: string;
  dataCadastro?: string;
  dataUltimaAtualizacao?: string;
}
