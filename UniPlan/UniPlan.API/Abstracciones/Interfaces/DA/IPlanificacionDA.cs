using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IPlanificacionDA
    {
        Task<Guid> CrearPlanificacion (PlanificacionRequest planificacion);
        Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario (Guid idUsuario);
        Task<PlanificacionResponse?> ObtenerPlanificacionPorId (Guid idPlanificacion);
        Task<bool> ExistePlanificacionUsuarioPeriodo (Guid idUsuario, int numeroPeriodo);
        Task AgregarCursoPlanificado (CursoPlanificadoRequest cursoPlanificado);
        Task EliminarCursoPlanificado (Guid idPlanificacion, Guid idCurso);
        Task<IEnumerable<CursoPlanificadoHorarioResponse>> ObtenerCursosPlanificadosPorPlanificacion (Guid idPlanificacion);
        Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario (Guid idUsuario);
    }
}