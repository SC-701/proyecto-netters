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
    public class RequisitosController : Controller, IRequisitosController
    {
        private IRequisitosFlujo _requisitosFlujo;
        private ILogger<RequisitosController> _logger;

        public RequisitosController(IRequisitosFlujo requisitosFlujo, ILogger<RequisitosController> logger)
        {
            _requisitosFlujo = requisitosFlujo;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _requisitosFlujo.Obtener();

            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }


        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Agregar([FromBody] RequisitosRequest requisito)
        {
            var resultado = await _requisitosFlujo.Agregar(requisito);
            return CreatedAtAction(nameof(ObtenerPorCurso), new
            {
                IdCarrera = resultado.IdCarrera,
                IdCurso = resultado.IdCurso
            }, null);
        }

        [HttpPut]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Editar([FromBody] RequisitosRequest requisito)
        {
            if (!await VerificarRequisitoExiste(requisito.IdCarrera, requisito.IdCurso, requisito.IdCursoRequisito))
                return NotFound("El requisito no existe.");

            var resultado = await _requisitosFlujo.Editar(requisito);
            return Ok(resultado);
        }

        [HttpDelete]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Eliminar([FromBody] RequisitosEliminarRequest requisito)
        {
            if (!await VerificarRequisitoExiste(requisito.IdCarrera, requisito.IdCurso, requisito.IdCursoRequisito))
                return NotFound("El requisito no existe.");

            var resultado = await _requisitosFlujo.Eliminar(requisito);
            return NoContent();
        }

        [HttpGet("PorCurso/{IdCarrera}/{IdCurso}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> ObtenerPorCurso([FromRoute] Guid IdCarrera, [FromRoute] Guid IdCurso)
        {
            var resultado = await _requisitosFlujo.ObtenerPorCurso(IdCarrera, IdCurso);
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("CursosQueLoRequieren/{IdCursoRequisito}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> ObtenerCursosQueLoRequieren([FromRoute] Guid IdCursoRequisito)
        {
            var resultado = await _requisitosFlujo.ObtenerCursosQueLoRequieren(IdCursoRequisito);
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }
        [HttpPut("Estado")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> CambiarEstado([FromBody] RequisitosEstadoRequest requisito)
        {
            var resultado = await _requisitosFlujo.CambiarEstado(requisito);
            return Ok(resultado);
        }


        #endregion

        #region Helpers

        private async Task<bool> VerificarRequisitoExiste(Guid IdCarrera, Guid IdCurso, Guid IdCursoRequisito)
        {
            var resultadoValidacion = false;
            var resultadoRequisitos = await _requisitosFlujo.ObtenerPorCurso(IdCarrera, IdCurso);

            if (resultadoRequisitos.Any(x =>
                x.IdCarrera == IdCarrera &&
                x.IdCurso == IdCurso &&
                x.IdCursoRequisito == IdCursoRequisito))
            {
                resultadoValidacion = true;
            }

            return resultadoValidacion;
        }

        #endregion
    }
}