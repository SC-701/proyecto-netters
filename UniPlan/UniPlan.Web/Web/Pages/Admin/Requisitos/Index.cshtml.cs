using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Requisitos;

public class IndexModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public string? Busqueda { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FiltroCarreraId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FiltroEstado { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PaginaActual { get; set; } = 1;
    public int TamañoPagina { get; set; } = 10;
    public int TotalRequisitos { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalRequisitos / TamañoPagina);
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalRequisitos);

    // TODO: cargar desde API
    public List<SelectListItem> CarreraOptions { get; set; } = new()
    {
        new("Ingeniería de Sistemas",    Guid.NewGuid().ToString()),
        new("Ingeniería Estructural",    Guid.NewGuid().ToString()),
        new("Análisis de Negocios",      Guid.NewGuid().ToString()),
    };

    public List<RequisitosResponse> Requisitos { get; set; } = new();

    public void OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        // TODO: bool? activo = FiltroEstado == "true" ? true : FiltroEstado == "false" ? false : null;
        // TODO: Requisitos      = await _requisitoService.ObtenerAsync(Busqueda, FiltroCarreraId, activo, PaginaActual, TamañoPagina);
        // TODO: TotalRequisitos = await _requisitoService.ContarAsync(Busqueda, FiltroCarreraId, activo);

        // Datos de ejemplo
        Requisitos = new List<RequisitosResponse>
        {
            new() { IdCarrera = Guid.NewGuid(), IdCurso = Guid.NewGuid(), IdCursoRequisito = Guid.NewGuid(), Carrera = "Ingeniería de Sistemas",  Curso = "CS302: Sistemas Operativos",    CursoRequisito = "CS201: Estructura de Datos",    EsCorequisito = false, Activo = true  },
            new() { IdCarrera = Guid.NewGuid(), IdCurso = Guid.NewGuid(), IdCursoRequisito = Guid.NewGuid(), Carrera = "Ingeniería Estructural",   Curso = "ENG405: Diseño en Concreto",     CursoRequisito = "ENG302: Resistencia de Mat.",   EsCorequisito = false, Activo = true  },
            new() { IdCarrera = Guid.NewGuid(), IdCurso = Guid.NewGuid(), IdCursoRequisito = Guid.NewGuid(), Carrera = "Análisis de Negocios",     Curso = "BA201: Modelos Predictivos",     CursoRequisito = "STAT101: Estadística Básica",   EsCorequisito = true,  Activo = false },
        };
        TotalRequisitos = 11;
    }
}
