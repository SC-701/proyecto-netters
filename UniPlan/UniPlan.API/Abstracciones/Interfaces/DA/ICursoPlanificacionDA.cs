using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA {
    public interface ICursoPlanificacionDA {
        Task<IEnumerable<CursoPlanificacionResponse>> Obtener();
        Task<CursoPlanificacionResponse> Obtener (Guid IdPlan);
        Task<Guid> Agregar (CursoPlanificacionRequest cursoplanificacion);
        Task<Guid> Editar (Guid IdPlan, Guid IdCurso, CursoPlanificacionRequest cursoplanificacion);
        Task<Guid> Eliminar (Guid IdPlan, Guid IdCurso);
        Task<CursoPlanificacionResponse> ObtenerUno (Guid IdPlan, Guid IdCurso);
    }
}