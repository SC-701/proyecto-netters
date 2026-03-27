using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class PerfilxUsuarioFlujo : IPerfilxUsuarioFlujo {
        private IPerfilxUsuarioDA _perfilxUsuarioDA;

        public PerfilxUsuarioFlujo (IPerfilxUsuarioDA perfilxUsuarioDA) {
            _perfilxUsuarioDA = perfilxUsuarioDA;
        }

        public async Task Agregar (PerfilxUsuarioRequest perfilxUsuario) {
            await _perfilxUsuarioDA.Agregar(perfilxUsuario);
        }

        public async Task Eliminar (PerfilxUsuarioRequest perfilxUsuario) {
            await _perfilxUsuarioDA.Eliminar(perfilxUsuario);
        } 
    }
}
