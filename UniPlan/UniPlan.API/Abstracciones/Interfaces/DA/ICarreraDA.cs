using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA {
    public interface ICarreraDA {
        Task<IEnumerable<CarreraResponse>> Obtener();
        Task<CarreraResponse> Obtener (Guid Id);
        Task<Guid> Agregar (CarreraRequest carrera);
        Task<Guid> Editar (Guid Id, CarreraRequest carrera);
        Task<Guid> Eliminar (Guid Id);
    }
}
