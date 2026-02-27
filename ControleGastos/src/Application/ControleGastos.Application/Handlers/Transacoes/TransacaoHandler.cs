using ControleGastos.Application.Configurations;
using ControleGastos.Application.Handlers.Transacoes.Interfaces;
using ControleGastos.Application.Handlers.Transacoes.Requests;
using ControleGastos.Application.Handlers.Transacoes.Responses;
using ControleGastos.Domain.Contexts.Categorias.Enums;
using ControleGastos.Domain.Contexts.Categorias.Interfaces;
using ControleGastos.Domain.Contexts.Pessoas.Interfaces;
using ControleGastos.Domain.Contexts.Transacoes;
using ControleGastos.Domain.Contexts.Transacoes.Interfaces;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Handlers.Transacoes
{
    public class TransacaoHandler : ITransacaoHandler
    {
        private readonly IPessoaRepository _pessoasRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public TransacaoHandler(
            IPessoaRepository pessoaRepository,
            ITransacaoRepository transacaoRepository,
            ICategoriaRepository categoriaRepository)
        {
            _pessoasRepository = pessoaRepository;
            _transacaoRepository = transacaoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ApiResponse<TransacaoResponse>> CriarTransacaoAsync(TransacaoRequest request)
        {
            if (string.IsNullOrEmpty(request.Descricao))
                return new ApiResponse<TransacaoResponse>(400, "Descricao não pode ser nulo ou vazio", null!);

            if (request.Valor <= 0)
                return new ApiResponse<TransacaoResponse>(400, "Valor não pode ser negativo", null!);

            if (!Enum.IsDefined(typeof(ETipo), request.Tipo))
                return new ApiResponse<TransacaoResponse>(400, "Tipo inválido", null!);

            var pessoa = await _pessoasRepository.ObterPessoaPorId(request.PessoaId);

            if (pessoa is null)
                return new ApiResponse<TransacaoResponse>(404, "Pessoa não encontrada", null!);
            
            if (pessoa.Idade < 18 && request.Tipo == ETipo.RECEITA)
                return new ApiResponse<TransacaoResponse>(400, "Menores de idade só podem registrar despesas", null!);

            var categoria = await _categoriaRepository.ObterCategoriaPorId(request.CategoriaId);
            
            if (categoria is null)
                return new ApiResponse<TransacaoResponse>(400, "Categoria não encontrada", null!);

            bool categoriaInvalida = (request.Tipo == ETipo.RECEITA && categoria.Finalidade == EFinalidade.DESPESA)
                                  || (request.Tipo == ETipo.DESPESA && categoria.Finalidade == EFinalidade.RECEITA);
            
            if (categoriaInvalida)
                return new ApiResponse<TransacaoResponse>(400, "Categoria incompatível com o tipo da transação", null!);

            var transacao = new Transacao(request.Descricao, request.Valor, request.Tipo, pessoa.Id, request.CategoriaId);

            pessoa.AdicionarTransacao(transacao);
            await _pessoasRepository.UnitOfWork.Commit();

            return new ApiResponse<TransacaoResponse>(201, "Transação realizada com sucesso", null!);
        }

        public async Task<ApiResponse<TransacaoResponse>> ObterTransacaoPorIdAsync(Guid transacaoId)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(transacaoId);

            if (transacao is null)
                return new ApiResponse<TransacaoResponse>(404, "Nenhuma transação encontrada", null!);

            var response = new TransacaoResponse
            {
                Categoria = transacao.Categoria,
                Descricao = transacao.Descricao,
                Tipo = transacao.Tipo,
                Valor = transacao.Valor
            };

            return new ApiResponse<TransacaoResponse>(200, string.Empty, response);
        }

        public async Task<ApiResponse<List<Transacao>>> ObterTransacoesPorPessoaIdAsync(Guid pessoaId, int pagina, int tamanhoPagina)
        {
            var pessoa = await _pessoasRepository.ObterPessoaPorId(pessoaId);

            if (pessoa is null)
                return new ApiResponse<List<Transacao>>(404, "Pessoa não encontrada", null!);

            var transacoes = await _transacaoRepository.ObterTransacaoPorPessoaId(pessoaId, pagina, tamanhoPagina);

            if (transacoes.Count == 0)
                return new ApiResponse<List<Transacao>>(404, "Nenhuma transação encontrada", null!);

            return new ApiResponse<List<Transacao>>(200, string.Empty, transacoes);
        }
    }
}
