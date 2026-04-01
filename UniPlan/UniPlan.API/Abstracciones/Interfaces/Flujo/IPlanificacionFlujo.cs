using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IPlanificacionFlujo
    {
        Task<Guid> AgregarPlanificacion(Planificacion planificacion);
        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificaciones();
        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuarioId(Guid idUsuario);
        Task<Guid> EditarPlanificacion(Guid id, Planificacion planificacion);
        Task<Guid> EliminarPlanificacion(Guid id);
    }
}