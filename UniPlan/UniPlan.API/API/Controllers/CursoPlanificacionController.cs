using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoPlanificacionController : Controller, ICursoPlanificacionController {
        private ICursoPlanificacionFlujo _cursoplanificacionFlujo;
        private ILogger<CursoPlanificacionController> _logger;

        public CursoPlanificacionController(ICursoPlanificacionFlujo cursoplanificacionFlujo, ILogger<CursoPlanificacionController> logger) {
            _cursoplanificacionFlujo = cursoplanificacionFlujo;
            _logger = logger;
        }

        #region CRUD

        // Roles: 1 - Usuario, 2 - Administrador (también tiene 1)

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Agregar ([FromBody] CursoPlanificacionRequest cursoplanificacion) {
            var resultado = await _cursoplanificacionFlujo.Agregar(cursoplanificacion);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{IdPlan}/{IdCurso}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Editar ([FromRoute] Guid IdPlan, [FromRoute] Guid IdCurso, [FromBody] CursoPlanificacionRequest cursoplanificacion) {
            if (!await VerificarCursoPlanificacionExiste(IdPlan, IdCurso))
                return NotFound("El curso de esta planificacion no existe.");
            var resultado = await _cursoplanificacionFlujo.Editar(IdPlan, IdCurso, cursoplanificacion);
            return Ok(resultado);
        }

        [HttpDelete("{IdPlan}/{IdCurso}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Eliminar ([FromRoute] Guid IdPlan, [FromRoute] Guid IdCurso) {
            if (!await VerificarCursoPlanificacionExiste(IdPlan, IdCurso))
                return NotFound("El curso de esta planificación no existe.");
            var resultado = await _cursoplanificacionFlujo.Eliminar(IdPlan, IdCurso);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener () {
            var resultado = await _cursoplanificacionFlujo.Obtener();
            if(!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{IdPlan}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener ([FromRoute] Guid IdPlan) {
            var resultado = await _cursoplanificacionFlujo.Obtener(IdPlan);
            return Ok(resultado);
        }
        #endregion

        #region Helpers
        private async Task<bool> VerificarCursoPlanificacionExiste(Guid IdPlan, Guid IdCurso) {
            var resultadoValidacion = false;
            var resultadoCursoPlanificacionExiste = await _cursoplanificacionFlujo.ObtenerUno(IdPlan, IdCurso);
            if (resultadoCursoPlanificacionExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
