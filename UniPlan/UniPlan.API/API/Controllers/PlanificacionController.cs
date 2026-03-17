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
    public class PlanificacionController : ControllerBase, IPlanificacionController
    {
        private readonly IPlanificacionFlujo _planificacionFlujo;
        private readonly ILogger<PlanificacionController> _logger;

        public PlanificacionController(IPlanificacionFlujo planificacionFlujo, ILogger<PlanificacionController> logger)
        {
            _planificacionFlujo = planificacionFlujo;
            _logger = logger;
        }

        [HttpPost("Crear")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CrearPlanificacion([FromBody] PlanificacionRequest planificacion)
        {
            var resultado = await _planificacionFlujo.CrearPlanificacion(planificacion);
            return StatusCode(201, resultado);
        }

        [HttpGet("PorUsuario/{idUsuario}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerPlanificacionesPorUsuario([FromRoute] Guid idUsuario)
        {
            var resultado = await _planificacionFlujo.ObtenerPlanificacionesPorUsuario(idUsuario);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpPost("AgregarCurso")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> AgregarCursoPlanificado([FromBody] CursoPlanificadoRequest cursoPlanificado)
        {
            await _planificacionFlujo.AgregarCursoPlanificado(cursoPlanificado);
            return Ok();
        }

        [HttpGet("HorarioUsuario/{idUsuario}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerHorarioUsuario([FromRoute] Guid idUsuario)
        {
            var resultado = await _planificacionFlujo.ObtenerHorarioUsuario(idUsuario);

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
    }
}