using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase, ICursoController
    {
        private readonly ICursoFlujo _cursoFlujo;
        private readonly ILogger<CursoController> _logger;

        public CursoController(ICursoFlujo cursoFlujo, ILogger<CursoController> logger)
        {
            _cursoFlujo = cursoFlujo;
            _logger = logger;
        }

        [HttpGet("PorPrograma/{idPrograma}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerCursosPorPrograma([FromRoute] Guid idPrograma)
        {
            var resultado = await _cursoFlujo.ObtenerCursosPorPrograma(idPrograma);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}