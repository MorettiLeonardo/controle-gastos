using ControleGastos.Application.Handlers.Transacoes.Interfaces;
using ControleGastos.Application.Handlers.Transacoes.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/transacao")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoHandler _transacaoHandler;

        public TransacaoController(ITransacaoHandler transacaoHandler)
        {
            _transacaoHandler = transacaoHandler;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarTransacao([FromBody] TransacaoRequest request)
        {
            var response = await _transacaoHandler.CriarTransacaoAsync(request);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{transacaoId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTransacoesPorId(Guid transacaoId)
        {
            var response = await _transacaoHandler.ObterTransacaoPorIdAsync(transacaoId);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("pessoa/{pessoaId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTransacoesPorPessoaId(Guid pessoaId, int pagina, int tamanhoPagina)
        {
            var response = await _transacaoHandler.ObterTransacoesPorPessoaIdAsync(pessoaId, pagina, tamanhoPagina);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }
    }
}
