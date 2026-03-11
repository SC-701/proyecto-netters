using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IUsuarioFlujo _usuarioFlujo;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioFlujo usuarioFlujo, ILogger<UsuarioController> logger)
        {
            _usuarioFlujo = usuarioFlujo;
            _logger = logger;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRequest usuario)
        {
            var resultado = await _usuarioFlujo.RegistrarUsuario(usuario);
             return StatusCode(201, resultado);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginRequest login)
        {
            var resultado = await _usuarioFlujo.IniciarSesion(login.Correo, login.Contrasenna);

            if (resultado == null)
                return Unauthorized("Credenciales incorrectas");

            return Ok(resultado);
        }
    }
}