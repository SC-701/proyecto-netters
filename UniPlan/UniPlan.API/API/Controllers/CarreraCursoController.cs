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
    public class CarreraCursoController : ControllerBase, ICarreraCursoController
    {
        private ICarreraCursoFlujo _carreraCursoFlujo;
        private ILogger<CarreraCursoController> _logger;

        public CarreraCursoController(ICarreraCursoFlujo carreraCursoFlujo, ILogger<CarreraCursoController> logger)
        {
            _carreraCursoFlujo = carreraCursoFlujo;
            _logger = logger;
        }

        #region "Operaciones"
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Agregar([FromBody] CarreraCursoRequest carreraCurso)
        {
            var resultado = await _carreraCursoFlujo.Agregar(carreraCurso);

            return CreatedAtAction(nameof(Obtener), new { IdCarrera = carreraCurso.IdCarrera, IdCurso = carreraCurso.IdCurso }, null);
        }

        [HttpPut("{IdCarrera}/{IdCurso}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Editar([FromRoute] Guid IdCarrera, [FromRoute] Guid IdCurso, [FromBody] CarreraCursoRequest carreraCurso)
        {
            if (!await VerificarCarreraCursoExiste(IdCarrera, IdCurso))
                return NotFound("La relación CarreraCurso no existe");

            var resultado = await _carreraCursoFlujo.Editar(IdCarrera, IdCurso, carreraCurso);
            return Ok(resultado);
        }

        [HttpDelete("{IdCarrera}/{IdCurso}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid IdCarrera, [FromRoute] Guid IdCurso)
        {
            if (!await VerificarCarreraCursoExiste(IdCarrera, IdCurso))
                return NotFound("La relación CarreraCurso no existe");

            var resultado = await _carreraCursoFlujo.Eliminar(IdCarrera, IdCurso);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _carreraCursoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{IdCarrera}/{IdCurso}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener([FromRoute] Guid IdCarrera, [FromRoute] Guid IdCurso)
        {
            var resultado = await _carreraCursoFlujo.Obtener(IdCarrera, IdCurso);
            return Ok(resultado);
        }
        #endregion "Operaciones"

        #region "Helpers"
        private async Task<bool> VerificarCarreraCursoExiste(Guid IdCarrera, Guid IdCurso)
        {
            var resultadoValidacion = false;
            var resultadoCarreraCursoExiste = await _carreraCursoFlujo.Obtener(IdCarrera, IdCurso);
            if (resultadoCarreraCursoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion "Helpers"
    }
}