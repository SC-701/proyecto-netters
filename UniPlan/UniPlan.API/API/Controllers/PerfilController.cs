using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : Controller, IPerfilController {
        private IPerfilFlujo _perfilFlujo;
        private ILogger<PerfilController> _logger;

        public PerfilController(IPerfilFlujo perfilFlujo, ILogger<PerfilController> logger) {
            _perfilFlujo = perfilFlujo;
            _logger = logger;
        }

        #region CRUD

        // Roles: 1 - Usuario, 2 - Administrador (también tiene 1)

        [HttpPost]
        public async Task<IActionResult> Agregar ([FromBody] PerfilRequest perfil) {
            var resultado = await _perfilFlujo.Agregar(perfil);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Editar ([FromRoute] int Id, [FromBody] PerfilRequest perfil) {
            if (!await VerificarPerfilExiste(Id))
                return NotFound("El perfil no existe.");
            var resultado = await _perfilFlujo.Editar(Id, perfil);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Eliminar ([FromRoute] int Id) {
            if (!await VerificarPerfilExiste(Id))
                return NotFound("El perfil no existe.");
            var resultado = await _perfilFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener () {
            var resultado = await _perfilFlujo.Obtener();
            if(!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener ([FromRoute] int Id) {
            var resultado = await _perfilFlujo.Obtener(Id);
            return Ok(resultado);
        }
        #endregion

        #region Helpers
        private async Task<bool> VerificarPerfilExiste(int Id) {
            var resultadoValidacion = false;
            var resultadoPerfilExiste = await _perfilFlujo.Obtener(Id);
            if (resultadoPerfilExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
