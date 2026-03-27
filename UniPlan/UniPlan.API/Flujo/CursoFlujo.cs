using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class CursoFlujo : ICursoFlujo {
        private ICursoDA _cursoDA;

        public CursoFlujo (ICursoDA cursoDA) {
            _cursoDA = cursoDA;
        }

        public async Task<Guid> Agregar (CursoRequest curso) {
            return await _cursoDA.Agregar(curso);
        }

        public async Task<Guid> Editar (Guid Id, CursoRequest curso) {
            return await _cursoDA.Editar(Id, curso);
        }

        public async Task<Guid> Eliminar (Guid Id) {
            return await _cursoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<CursoResponse>> Obtener () {
            return await _cursoDA.Obtener();
        }

        public async Task<CursoResponse> Obtener (Guid Id) {
            return await _cursoDA.Obtener(Id);
        }

        public async Task<CursoResponse> ObtenerSigla (string Sigla) {
            return await _cursoDA.ObtenerSigla(Sigla);
        }
    }
}
