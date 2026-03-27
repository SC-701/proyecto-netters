using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API {
    public interface ICarreraController {
        Task<IActionResult> Agregar (CarreraRequest carrera);
        Task<IActionResult> Editar (Guid Id, CarreraRequest carrera);
        Task<IActionResult> Eliminar (Guid Id);
        Task<IActionResult> Obtener ();
        Task<IActionResult> Obtener (Guid Id);
    }
}
