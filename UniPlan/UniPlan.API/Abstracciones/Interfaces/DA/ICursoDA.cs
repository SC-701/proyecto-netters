using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA {
    public interface ICursoDA {
        Task<IEnumerable<CursoResponse>> Obtener ();
        Task<CursoResponse> Obtener (Guid Id);
        Task<CursoResponse> ObtenerSigla (string Sigla);
        Task<Guid> Agregar (CursoRequest curso);
        Task<Guid> Editar (Guid Id, CursoRequest curso);
        Task<Guid> Eliminar (Guid Id);
    }
}
