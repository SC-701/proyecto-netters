using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IPlanificacionDA
    {
        Task<Guid> AgregarPlanificacion(Planificacion planificacion);
        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificaciones();
        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuarioId(Guid idUsuario);
        Task<Guid> EditarPlanificacion(Guid id, Planificacion planificacion);
        Task<Guid> EliminarPlanificacion(Guid id);
    }
}