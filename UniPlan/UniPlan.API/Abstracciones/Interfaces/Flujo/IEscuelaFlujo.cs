using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo {
    public interface IEscuelaFlujo {
        Task<IEnumerable<EscuelaResponse>> Obtener ();
        Task<EscuelaResponse> Obtener (Guid Id);
        Task<Guid> Agregar (EscuelaRequest escuela);
        Task<Guid> Editar (Guid Id, EscuelaRequest escuela);
        Task<Guid> Eliminar (Guid Id);

        Task<Guid> Activar(Guid Id);
    }
}
