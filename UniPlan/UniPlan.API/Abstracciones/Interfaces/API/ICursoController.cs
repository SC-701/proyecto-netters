using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API {
    public interface ICursoController {
        Task<IActionResult> Agregar (CursoRequest curso);
        Task<IActionResult> Editar (Guid Id, CursoRequest curso);
        Task<IActionResult> Eliminar (Guid Id);
        Task<IActionResult> Obtener ();
        Task<IActionResult> Obtener (Guid Id);
        Task<IActionResult> ObtenerSigla (string Sigla);
    }
}
