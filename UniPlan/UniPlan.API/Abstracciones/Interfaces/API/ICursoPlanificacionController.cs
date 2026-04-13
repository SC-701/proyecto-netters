using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API {
    public interface ICursoPlanificacionController {
        Task<IActionResult> Agregar (CursoPlanificacionRequest cursoplanificacion);
        Task<IActionResult> Editar (Guid IdPlan, Guid IdCurso, CursoPlanificacionRequest cursoplanificacion);
        Task<IActionResult> Eliminar (Guid IdPlan, Guid IdCurso);
        Task<IActionResult> Obtener ();
        Task<IActionResult> Obtener (Guid IdPlan);
    }
}
