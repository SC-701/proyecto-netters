using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoAprobadoController : ControllerBase, ICursoAprobadoController
    {
        private readonly ICursoAprobadoFlujo _cursoAprobadoFlujo;
        private readonly ILogger<CursoAprobadoController> _logger;

        public CursoAprobadoController(ICursoAprobadoFlujo cursoAprobadoFlujo, ILogger<CursoAprobadoController> logger)
        {
            _cursoAprobadoFlujo = cursoAprobadoFlujo;
            _logger = logger;
        }

        [HttpPost("Registrar")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> RegistrarCursoAprobado([FromBody] CursoAprobadoRequest cursoAprobado)
        {
            try
            {
                await _cursoAprobadoFlujo.RegistrarCursoAprobado(cursoAprobado);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PorUsuario/{idUsuario}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerCursosAprobadosPorUsuario([FromRoute] Guid idUsuario)
        {
            var resultado = await _cursoAprobadoFlujo.ObtenerCursosAprobadosPorUsuario(idUsuario);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}
