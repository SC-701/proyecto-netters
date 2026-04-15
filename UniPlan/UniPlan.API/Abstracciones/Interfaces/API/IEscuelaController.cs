using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API {
    public interface IEscuelaController {
        Task<IActionResult> Agregar (EscuelaRequest escuela);
        Task<IActionResult> Editar (Guid Id, EscuelaRequest escuela);
        Task<IActionResult> Eliminar (Guid Id);
        Task<IActionResult> Obtener ();
        Task<IActionResult> Obtener (Guid Id);

        Task<IActionResult> Activar(Guid Id);
    }
}
