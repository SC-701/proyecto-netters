using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<Guid> RegistrarUsuario (UsuarioRequest usuario);
        Task<UsuarioResponse?> IniciarSesion (string correo, string contrasenna);
        Task<UsuarioResponse?> ObtenerUsuarioPorId (Guid idUsuario);

    }
}
