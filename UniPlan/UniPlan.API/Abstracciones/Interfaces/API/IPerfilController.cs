using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API {
    public interface IPerfilController {
        Task<IActionResult> Agregar (PerfilRequest perfil);
        Task<IActionResult> Editar (int Id, PerfilRequest perfil);
        Task<IActionResult> Eliminar (int Id);
        Task<IActionResult> Obtener ();
        Task<IActionResult> Obtener (int Id);
    }
}
