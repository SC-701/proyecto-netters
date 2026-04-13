using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class CursoPlanificacionFlujo : ICursoPlanificacionFlujo {
        private ICursoPlanificacionDA _cursoplanificacionDA;

        public CursoPlanificacionFlujo (ICursoPlanificacionDA cursoplanificacionDA) {
            _cursoplanificacionDA = cursoplanificacionDA;
        }

        public async Task<Guid> Agregar (CursoPlanificacionRequest cursoplanificacion) {
            return await _cursoplanificacionDA.Agregar(cursoplanificacion);
        }

        public async Task<Guid> Editar (Guid IdPlan, Guid IdCurso, CursoPlanificacionRequest cursoplanificacion) {
            return await _cursoplanificacionDA.Editar(IdPlan, IdCurso, cursoplanificacion);
        }

        public async Task<Guid> Eliminar (Guid IdPlan, Guid IdCurso) {
            return await _cursoplanificacionDA.Eliminar(IdPlan, IdCurso);
        }

        public async Task<IEnumerable<CursoPlanificacionResponse>> Obtener () {
            return await _cursoplanificacionDA.Obtener();
        }

        public async Task<CursoPlanificacionResponse> Obtener (Guid IdPlan) {
            return await _cursoplanificacionDA.Obtener(IdPlan);
        }

        public async Task<CursoPlanificacionResponse> ObtenerUno (Guid IdPlan, Guid IdCurso) {
            return await _cursoplanificacionDA.ObtenerUno(IdPlan, IdCurso);
        }
    }
}
