using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;



namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;


        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<UsuarioResponse?> IniciarSesion(string correo, string contrasenna)
        {
            return await _usuarioDA.IniciarSesion(correo, contrasenna);
        }

        public async Task<Guid> RegistrarUsuario(UsuarioRequest usuario)
        {
            return await _usuarioDA.RegistrarUsuario(usuario);
        }
    }
}
