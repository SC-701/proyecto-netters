using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo {
    public interface IPerfilxUsuarioFlujo {
        Task Agregar (PerfilxUsuarioRequest perfilxUsuario);
        Task Eliminar (PerfilxUsuarioRequest perfilxUsuario);
    }
}
