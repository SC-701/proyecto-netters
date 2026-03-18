using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICursoAprobadoFlujo
    {
        Task RegistrarCursoAprobado(CursoAprobadoRequest cursoAprobado);
        Task<IEnumerable<CursoAprobadoResponse>> ObtenerCursosAprobadosPorUsuario(Guid idUsuario);
    }
}
