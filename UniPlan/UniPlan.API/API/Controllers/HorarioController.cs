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
    public class HorarioController : Controller, IHorarioController
    {
        private IHorarioFlujo _horarioFlujo;
        private ILogger<HorarioController> _logger;

        public HorarioController(IHorarioFlujo horarioFlujo, ILogger<HorarioController> logger)
        {
            _horarioFlujo = horarioFlujo;
            _logger = logger;
        }

        #region CRUD
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Agregar([FromBody] HorarioRequest horario)
        {
            var resultado = await _horarioFlujo.Agregar(horario);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, resultado);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] HorarioRequest horario)
        {
            if (!await VerificarHorarioExiste(Id))
                return NotFound("El horario no existe.");
            var resultado = await _horarioFlujo.Editar(Id, horario);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarHorarioExiste(Id))
                return NotFound("El horario no existe.");
            var resultado = await _horarioFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _horarioFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _horarioFlujo.Obtener(Id);
            return Ok(resultado);
        }

        [HttpPut("{Id}/activar")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Activar([FromRoute] Guid Id)
        {
            if (!await VerificarHorarioExiste(Id))
                return NotFound("El horario no existe.");
            var resultado = await _horarioFlujo.Activar(Id);
            return Ok(resultado);
        }
        #endregion

        #region Helpers
        private async Task<bool> VerificarHorarioExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoHorarioExiste = await _horarioFlujo.Obtener(Id);
            if (resultadoHorarioExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion
    }
}