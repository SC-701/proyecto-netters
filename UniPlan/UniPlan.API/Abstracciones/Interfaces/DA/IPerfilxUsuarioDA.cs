using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA {
    public interface IPerfilxUsuarioDA {
        Task Agregar (PerfilxUsuarioRequest perfilxUsuario);
        Task Eliminar (PerfilxUsuarioRequest perfilxUsuario);
    }
}
