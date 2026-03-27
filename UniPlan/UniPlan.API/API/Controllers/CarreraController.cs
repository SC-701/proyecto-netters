using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarreraController : Controller, ICarreraController {
        private ICarreraFlujo _carreraFlujo;
        private ILogger<CarreraController> _logger;

        public CarreraController(ICarreraFlujo carreraFlujo, ILogger<CarreraController> logger) {
            _carreraFlujo = carreraFlujo;
            _logger = logger;
        }

        #region CRUD

        // Roles: 1 - Usuario, 2 - Administrador (también tiene 1)

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Agregar ([FromBody] CarreraRequest carrera) {
            var resultado = await _carreraFlujo.Agregar(carrera);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Editar ([FromRoute] Guid Id, [FromBody] CarreraRequest carrera) {
            if (!await VerificarCarreraExiste(Id))
                return NotFound("La carrera no existe.");
            var resultado = await _carreraFlujo.Editar(Id, carrera);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Eliminar ([FromRoute] Guid Id) {
            if (!await VerificarCarreraExiste(Id))
                return NotFound("La carrera no existe.");
            var resultado = await _carreraFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener () {
            var resultado = await _carreraFlujo.Obtener();
            if(!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener ([FromRoute] Guid Id) {
            var resultado = await _carreraFlujo.Obtener(Id);
            return Ok(resultado);
        }
        #endregion

        #region Helpers
        private async Task<bool> VerificarCarreraExiste(Guid Id) {
            var resultadoValidacion = false;
            var resultadoCarreraExiste = await _carreraFlujo.Obtener(Id);
            if (resultadoCarreraExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
