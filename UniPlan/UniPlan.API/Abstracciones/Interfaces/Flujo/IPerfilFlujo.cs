using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo {
    public interface IPerfilFlujo {
        Task<IEnumerable<PerfilResponse>> Obtener ();
        Task<PerfilResponse> Obtener (int Id);
        Task<int> Agregar (PerfilRequest perfil);
        Task<int> Editar (int Id, PerfilRequest perfil);
        Task<int> Eliminar (int Id);
    }
}
