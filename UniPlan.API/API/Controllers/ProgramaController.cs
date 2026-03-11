using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramaController : ControllerBase, IProgramaController
    {
        private IProgramaFlujo _programaFlujo;
        private ILogger<ProgramaController> _logger;

        public ProgramaController(IProgramaFlujo programaFlujo, ILogger<ProgramaController> logger)
        {
            _programaFlujo = programaFlujo;
            _logger = logger;
        }

        #region Operaciones
        [HttpGet]
        public async Task<IActionResult> ObtenerProgramas()
        {
            var resultado = await _programaFlujo.ObtenerProgramas();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
        #endregion
    }
}