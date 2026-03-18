using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ICursoAprobadoDA
    {
        Task RegistrarCursoAprobado(CursoAprobadoRequest cursoAprobado);
        Task<IEnumerable<CursoAprobadoResponse>> ObtenerCursosAprobadosPorUsuario(Guid idUsuario);
    }
}
