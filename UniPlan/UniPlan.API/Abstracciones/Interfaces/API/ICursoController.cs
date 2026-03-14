using Microsoft.AspNetCore.Mvc;

public interface ICursoController
{
    Task<IActionResult> ObtenerCursosPorPrograma(Guid idPrograma);
}