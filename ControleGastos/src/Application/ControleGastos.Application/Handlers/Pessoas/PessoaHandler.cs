using ControleGastos.Application.Configurations;
using ControleGastos.Application.Handlers.Pessoas.Interfaces;
using ControleGastos.Application.Handlers.Pessoas.Requests;
using ControleGastos.Application.Handlers.Pessoas.Responses;
using ControleGastos.Domain.Contexts.Categorias.Enums;
using ControleGastos.Domain.Contexts.Pessoas;
using ControleGastos.Domain.Contexts.Pessoas.Interfaces;

namespace ControleGastos.Application.Handlers.Pessoas
{
    public class PessoaHandler : IPessoaHandler
    {
        private readonly IPessoaRepository _pessoasRepository;

        public PessoaHandler(IPessoaRepository pessoaRepository)
        {
            _pessoasRepository = pessoaRepository;
        }

        public async Task<ApiResponse<PessoaResponse>> CriarPessoaAsync(PessoaRequest request)
        {
            if(string.IsNullOrEmpty(request.Nome))
                return new ApiResponse<PessoaResponse>(400, "Nome não pode ser nulo ou vazio", null!);

            if (request.Idade < 0)
                return new ApiResponse<PessoaResponse>(400, "Idade não pode ser negativa", null!);

            var pessoa = new Pessoa(request.Nome, request.Idade);

            _pessoasRepository.Adicionar(pessoa);
            await _pessoasRepository.UnitOfWork.Commit();

            var response = new PessoaResponse
            {
                Nome = request.Nome,
                Idade = request.Idade,
            };

            return new ApiResponse<PessoaResponse>(201, "Pessoa criada com sucesso", response);
        }

        public async Task<ApiResponse<ConsultaTotaisPessoasResponse>> ObterTodasPessoasComTransacoesAsync()
        {
            var pessoas = await _pessoasRepository.ObterTodasPessoasComTransacoes();

            var lista = pessoas.Select(p => new PessoaTotaisResponse
            {
                PessoaId = p.Id,
                Nome = p.Nome,
                TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == ETipo.RECEITA)
                    .Sum(t => t.Valor),

                TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == ETipo.DESPESA)
                    .Sum(t => t.Valor)
            }).ToList();

            var totalGeralReceitas = lista.Sum(x => x.TotalReceitas);
            var totalGeralDespesas = lista.Sum(x => x.TotalDespesas);

            var response = new ConsultaTotaisPessoasResponse
            {
                Pessoas = lista,
                TotalGeralReceitas = totalGeralReceitas,
                TotalGeralDespesas = totalGeralDespesas
            };

            return new ApiResponse<ConsultaTotaisPessoasResponse>(200, string.Empty, response);
        }

        public async Task<ApiResponse<PessoaResponse>> ObterPessoasPorIdAsync(Guid pessoaId)
        {
            var pessoa = await _pessoasRepository.ObterPessoaPorId(pessoaId);

            if (pessoa is null)
                return new ApiResponse<PessoaResponse>(404, "Pessoa não encontrada", null!);

            var response = new PessoaResponse
            {
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
            };

            return new ApiResponse<PessoaResponse>(200, string.Empty, response);
        }

        public async Task<ApiResponse<PessoaResponse>> AtualizarPessoaAsync(Guid pessoaId, PessoaRequest request)
        {
            var pessoa = await _pessoasRepository.ObterPessoaPorId(pessoaId);

            if (pessoa is null)
                return new ApiResponse<PessoaResponse>(404, "Pessoa não encontrada", null!);

            pessoa.AtualizarDados(request.Nome, request.Idade);

            await _pessoasRepository.UnitOfWork.Commit();

            var response = new PessoaResponse
            {
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };

            return new ApiResponse<PessoaResponse>(200, "Pessoa atualizada com sucesso", response);
        }

        public async Task<ApiResponse<object>> DeletarPessoaAsync(Guid pessoaId)
        {
            var pessoa = await _pessoasRepository.ObterPessoaPorId(pessoaId);

            if (pessoa is null)
                return new ApiResponse<object>(404, "Pessoa não encontrada", null!);

            _pessoasRepository.Remover(pessoa);
            await _pessoasRepository.UnitOfWork.Commit();

            return new ApiResponse<object>(204, "Pessoa excluida com sucesso", null!);
        }
    }
}
