using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface ICursoAprobadoController
    {
        Task<IActionResult> RegistrarCursoAprobado(CursoAprobadoRequest cursoAprobado);
        Task<IActionResult> ObtenerCursosAprobadosPorUsuario(Guid idUsuario);
    }
}
