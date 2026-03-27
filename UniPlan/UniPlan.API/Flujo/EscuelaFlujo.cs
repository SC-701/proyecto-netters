using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class EscuelaFlujo : IEscuelaFlujo {
        private IEscuelaDA _escuelaDA;

        public EscuelaFlujo (IEscuelaDA escuelaDA) {
            _escuelaDA = escuelaDA;
        }

        public async Task<Guid> Agregar (EscuelaRequest escuela) {
            return await _escuelaDA.Agregar(escuela);
        }

        public async Task<Guid> Editar (Guid Id, EscuelaRequest escuela) {
            return await _escuelaDA.Editar(Id, escuela);
        }

        public async Task<Guid> Eliminar (Guid Id) {
            return await _escuelaDA.Eliminar(Id);
        }

        public async Task<IEnumerable<EscuelaResponse>> Obtener () {
            return await _escuelaDA.Obtener();
        }

        public async Task<EscuelaResponse> Obtener (Guid Id) {
            return await _escuelaDA.Obtener(Id);
        }
    }
}
