using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "2")]
    public class PerfilxUsuarioController : Controller, IPerfilxUsuarioController {
        private IPerfilxUsuarioFlujo _perfilxusuarioFlujo;
        private ILogger<PerfilxUsuarioController> _logger;

        public PerfilxUsuarioController(IPerfilxUsuarioFlujo perfilxusuarioFlujo, ILogger<PerfilxUsuarioController> logger) {
            _perfilxusuarioFlujo = perfilxusuarioFlujo;
            _logger = logger;
        }

        #region CRUD

        // Roles: 1 - Usuario, 2 - Administrador (también tiene 1)

        [HttpPost]
        public async Task<IActionResult> Agregar ([FromBody] PerfilxUsuarioRequest perfilxusuario) {
            await _perfilxusuarioFlujo.Agregar(perfilxusuario);
            return Ok();
        }

        [HttpDelete("{idUsuario}/{IdPerfil}")]
        public async Task<IActionResult> Eliminar ([FromRoute] Guid idUsuario, [FromRoute] int idPerfil) {
            await _perfilxusuarioFlujo.Eliminar(new PerfilxUsuarioRequest {
                IdUsuario = idUsuario,
                IdPerfil = idPerfil
            });
            return NoContent();
        }
        #endregion

    }
}
