using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IPlanificacionController
    {
        Task<IActionResult> CrearPlanificacion (PlanificacionRequest planificacion);
        Task<IActionResult> ObtenerPlanificacionesPorUsuario (Guid idUsuario);
        Task<IActionResult> AgregarCursoPlanificado (CursoPlanificadoRequest cursoPlanificado);
        Task<IActionResult> EliminarCursoPlanificado (Guid idPlanificacion, Guid idCurso);
        Task<IActionResult> ObtenerHorarioUsuario (Guid idUsuario);
    }
}