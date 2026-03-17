using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IPlanificacionFlujo
    {
        Task<Guid> CrearPlanificacion(PlanificacionRequest planificacion);

        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario(Guid idUsuario);

        Task AgregarCursoPlanificado(CursoPlanificadoRequest cursoPlanificado);

        Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario(Guid idUsuario);
    }
}