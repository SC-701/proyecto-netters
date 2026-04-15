using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : Controller, ICursoController {
        private ICursoFlujo _cursoFlujo;
        private ILogger<CursoController> _logger;

        public CursoController (ICursoFlujo cursoFlujo, ILogger<CursoController> logger) {
            _cursoFlujo = cursoFlujo;
            _logger = logger;
        }

        #region CRUD

        // Roles: 1 - Usuario, 2 - Administrador (también tiene 1)

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Agregar ([FromBody] CursoRequest curso) {
            var resultado = await _cursoFlujo.Agregar(curso);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Editar ([FromRoute] Guid Id, [FromBody] CursoRequest curso) {
            if (!await VerificarCursoExiste(Id))
                return NotFound("El curso no existe.");
            var resultado = await _cursoFlujo.Editar(Id, curso);
            return Ok(resultado);
        }

        [HttpPut("Activar/{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Activar ([FromRoute] Guid Id) {
            if (!await VerificarCursoExiste(Id))
                return NotFound("El curso no existe.");
            var resultado = await _cursoFlujo.Activar(Id);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Eliminar ([FromRoute] Guid Id) {
            if (!await VerificarCursoExiste(Id))
                return NotFound("El curso no existe.");
            var resultado = await _cursoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener () {
            var resultado = await _cursoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener ([FromRoute] Guid Id) {
            var resultado = await _cursoFlujo.Obtener(Id);
            return Ok(resultado);
        }

        [HttpGet("Sigla/{Sigla}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerSigla ([FromRoute] string Sigla) {
            var resultado = await _cursoFlujo.ObtenerSigla(Sigla);
            return Ok(resultado);
        }

        #endregion

        #region Helpers
        private async Task<bool> VerificarCursoExiste (Guid Id) {
            var resultadoValidacion = false;
            var resultadoCursoExiste = await _cursoFlujo.Obtener(Id);
            if (resultadoCursoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}
