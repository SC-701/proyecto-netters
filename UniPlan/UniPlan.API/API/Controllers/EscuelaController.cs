using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EscuelaController : Controller, IEscuelaController {
        private IEscuelaFlujo _escuelaFlujo;
        private ILogger<EscuelaController> _logger;

        public EscuelaController (IEscuelaFlujo escuelaFlujo, ILogger<EscuelaController> logger) {
            _escuelaFlujo = escuelaFlujo;
            _logger = logger;
        }

        #region CRUD

        // Roles: 1 - Usuario, 2 - Administrador (también tiene 1)

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Agregar ([FromBody] EscuelaRequest escuela) {
            var resultado = await _escuelaFlujo.Agregar(escuela);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Editar ([FromRoute] Guid Id, [FromBody] EscuelaRequest escuela) {
            if (!await VerificarEscuelaExiste(Id))
                return NotFound("La escuela no existe.");
            var resultado = await _escuelaFlujo.Editar(Id, escuela);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Eliminar ([FromRoute] Guid Id) {
            if (!await VerificarEscuelaExiste(Id))
                return NotFound("La escuela no existe.");
            var resultado = await _escuelaFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Obtener () {
            var resultado = await _escuelaFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener ([FromRoute] Guid Id) {
            var resultado = await _escuelaFlujo.Obtener(Id);
            return Ok(resultado);
        }

        #endregion

        #region Helpers
        private async Task<bool> VerificarEscuelaExiste (Guid Id) {
            var resultadoValidacion = false;
            var resultadoEscuelaExiste = await _escuelaFlujo.Obtener(Id);
            if (resultadoEscuelaExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
