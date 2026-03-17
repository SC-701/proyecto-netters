using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IPlanificacionDA
    {
        Task<Guid> CrearPlanificacion(PlanificacionRequest planificacion);

        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario(Guid idUsuario);

        Task AgregarCursoPlanificado(CursoPlanificadoRequest cursoPlanificado);

        Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario(Guid idUsuario);
    }
}