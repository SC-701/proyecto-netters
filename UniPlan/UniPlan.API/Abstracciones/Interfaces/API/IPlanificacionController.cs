using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IPlanificacionController
    {
        Task<IActionResult> Agregar(PlanificacionBase planificacion);
        Task<IActionResult> Editar(Guid Id, PlanificacionBase planificacion);
        Task<IActionResult> Eliminar(Guid Id);
        Task<IActionResult> Obtener();
        Task<IActionResult> ObtenerPorUsuario();
    }
}