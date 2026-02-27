using ControleGastos.Application.Handlers.Categorias.Interface;
using ControleGastos.Application.Handlers.Categorias.Request;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaHandler _categoriaHandler;

        public CategoriaController(ICategoriaHandler categoriaHandler)
        {
            _categoriaHandler = categoriaHandler;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarCategoria([FromBody] CategoriaRequest request)
        {
            var response = await _categoriaHandler.CriarCategoriaAsync(request);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodasCategoria([FromQuery] int pagina, int tamanhoPagina)
        {
            var response = await _categoriaHandler.ObterTodasCategoriasAsync(pagina, tamanhoPagina);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{categoriaId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterCategoriaPorId(Guid categoriaId)
        {
            var response = await _categoriaHandler.ObterCategoriaPorIdAsync(categoriaId);

            if (response is null) return BadRequest(response);

            return Ok(response);
        }
        

        [HttpGet("total")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarTotaisPorCategoria()
        {
            var response = await _categoriaHandler.ConsultarTotaisPorCategoriaAsync();

            if (response is null) return BadRequest(response);

            return Ok(response);
        }
    }
}
