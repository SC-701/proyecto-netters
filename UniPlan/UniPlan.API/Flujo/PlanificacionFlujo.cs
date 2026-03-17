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

        public async Task<Guid> CrearPlanificacion(PlanificacionRequest planificacion)
        {
            return await _planificacionDA.CrearPlanificacion(planificacion);
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario(Guid idUsuario)
        {
            return await _planificacionDA.ObtenerPlanificacionesPorUsuario(idUsuario);
        }

        public async Task AgregarCursoPlanificado(CursoPlanificadoRequest cursoPlanificado)
        {
            await _planificacionDA.AgregarCursoPlanificado(cursoPlanificado);
        }

        public async Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario(Guid idUsuario)
        {
            return await _planificacionDA.ObtenerHorarioUsuario(idUsuario);
        }
    }
}