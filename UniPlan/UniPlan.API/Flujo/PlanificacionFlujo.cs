using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class PlanificacionFlujo : IPlanificacionFlujo
    {
        private readonly IPlanificacionDA _planificacionDA;

        public PlanificacionFlujo(IPlanificacionDA planificacionDA)
        {
            _planificacionDA = planificacionDA;
        }

        public async Task<Guid> AgregarPlanificacion(Planificacion planificacion)
        {
            return await _planificacionDA.AgregarPlanificacion(planificacion);
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificaciones()
        {
            return await _planificacionDA.ObtenerPlanificaciones();
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuarioId(Guid idUsuario)
        {
            return await _planificacionDA.ObtenerPlanificacionesPorUsuarioId(idUsuario);
        }

        public async Task<Guid> EditarPlanificacion(Guid id, Planificacion planificacion)
        {
            return await _planificacionDA.EditarPlanificacion(id, planificacion);
        }

        public async Task<Guid> EliminarPlanificacion(Guid id)
        {
            return await _planificacionDA.EliminarPlanificacion(id);
        }
    }
}