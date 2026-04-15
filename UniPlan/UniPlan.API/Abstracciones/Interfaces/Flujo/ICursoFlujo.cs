using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo {
    public interface ICursoFlujo {
        Task<IEnumerable<CursoResponse>> Obtener ();
        Task<CursoResponse> Obtener (Guid Id);
        Task<CursoResponse> ObtenerSigla (string Sigla);
        Task<Guid> Agregar (CursoRequest curso);
        Task<Guid> Editar (Guid Id, CursoRequest curso);
        Task<Guid> Eliminar (Guid Id);
        Task<Guid> Activar (Guid Id);
    }
}
