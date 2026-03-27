using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class PerfilFlujo : IPerfilFlujo {
        private IPerfilDA _perfilDA;

        public PerfilFlujo (IPerfilDA perfilDA) {
            _perfilDA = perfilDA;
        }

        public async Task<int> Agregar (PerfilRequest perfil) {
            return await _perfilDA.Agregar(perfil);
        }

        public async Task<int> Editar (int Id, PerfilRequest perfil) {
            return await _perfilDA.Editar(Id, perfil);
        }

        public async Task<int> Eliminar (int Id) {
            return await _perfilDA.Eliminar(Id);
        }

        public async Task<IEnumerable<PerfilResponse>> Obtener () {
            return await _perfilDA.Obtener();
        }

        public async Task<PerfilResponse> Obtener (int Id) {
            return await _perfilDA.Obtener(Id);
        }
    }
}
