using ControleGastos.Application.Handlers.Pessoas.Interfaces;
using ControleGastos.Application.Handlers.Pessoas.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaHandler _pessoaHandler;

        public PessoaController(IPessoaHandler pessoaHandler)
        {
            _pessoaHandler = pessoaHandler;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarPessoa([FromBody] PessoaRequest request)
        {
            var response = await _pessoaHandler.CriarPessoaAsync(request);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPessoas()
        {
            var response = await _pessoaHandler.ObterTodasPessoasComTransacoesAsync();

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{pessoaId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPessoaPorId(Guid pessoaId)
        {
            var response = await _pessoaHandler.ObterPessoasPorIdAsync(pessoaId);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("{pessoaId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarPessoa(
            Guid pessoaId,
            [FromBody] PessoaRequest request)
        {
            var response = await _pessoaHandler.AtualizarPessoaAsync(pessoaId, request);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{pessoaId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarPessoa(Guid pessoaId)
        {
            var response = await _pessoaHandler.DeletarPessoaAsync(pessoaId);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }
    }
}