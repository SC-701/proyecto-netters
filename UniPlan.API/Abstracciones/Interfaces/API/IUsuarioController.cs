using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {


        Task<IActionResult> RegistrarUsuario(UsuarioRequest usuario);
        Task<IActionResult> IniciarSesion(LoginRequest login);

    }
}
