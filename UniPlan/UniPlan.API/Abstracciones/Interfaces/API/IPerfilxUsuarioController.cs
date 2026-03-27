using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API {
    public interface IPerfilxUsuarioController {
        Task<IActionResult> Agregar (PerfilxUsuarioRequest perfilxUsuario);
        Task<IActionResult> Eliminar (Guid idUsuario, int idPerfil);
    }
}
