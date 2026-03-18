using Microsoft.AspNetCore.Mvc;

public interface ICursoController
{
    Task<IActionResult> ObtenerCursosPorPrograma (Guid idPrograma);
    Task<IActionResult> ObtenerCursoDetalle (Guid idCurso);
    Task<IActionResult> ObtenerCursosDisponiblesParaUsuario (Guid idUsuario);
}