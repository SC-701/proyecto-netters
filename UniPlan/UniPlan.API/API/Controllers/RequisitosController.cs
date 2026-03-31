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


    }
