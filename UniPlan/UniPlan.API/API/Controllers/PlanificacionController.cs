using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionController : Controller, IPlanificacionController
    {
        private readonly IPlanificacionFlujo _planificacionFlujo;

        public PlanificacionController(IPlanificacionFlujo planificacionFlujo)
        {
            _planificacionFlujo = planificacionFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _planificacionFlujo.ObtenerPlanificaciones();
            return Ok(resultado);
        }

        [HttpGet("ObtenerPorUsuario")]
        public async Task<IActionResult> ObtenerPorUsuario()
        {
            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim))
            {
                return Unauthorized("No se pudo obtener el usuario autenticado");
            }

            var resultado = await _planificacionFlujo.ObtenerPlanificacionesPorUsuarioId(Guid.Parse(idUsuarioClaim));
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] PlanificacionBase planificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim))
            {
                return Unauthorized("No se pudo obtener el usuario autenticado");
            }

            var nuevaPlanificacion = new Planificacion
            {
                Periodo = planificacion.Periodo,
                Anio = planificacion.Anio,
                Estado = planificacion.Estado,
                IdUsuario = Guid.Parse(idUsuarioClaim),
                Activo = true
            };

            var resultado = await _planificacionFlujo.AgregarPlanificacion(nuevaPlanificacion);
            return StatusCode(StatusCodes.Status201Created, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] PlanificacionBase planificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim))
            {
                return Unauthorized("No se pudo obtener el usuario autenticado");
            }

            var planificacionEditar = new Planificacion
            {
                Id = id,
                Periodo = planificacion.Periodo,
                Anio = planificacion.Anio,
                Estado = planificacion.Estado,
                IdUsuario = Guid.Parse(idUsuarioClaim),
                Activo = true
            };

            var resultado = await _planificacionFlujo.EditarPlanificacion(id, planificacionEditar);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultado = await _planificacionFlujo.EliminarPlanificacion(id);
            return Ok(resultado);
        }
    }
}