using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Cursos;

[Authorize]
public class IndexModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public string? Busqueda { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FiltroEstado { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PaginaActual { get; set; } = 1;
    public int TamañoPagina { get; set; } = 10;
    public int TotalCursos { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalCursos / TamañoPagina);
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalCursos);

    public List<CursoResponse> Cursos { get; set; } = new();

    public void OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        // TODO: bool? activo = FiltroEstado == "true" ? true : FiltroEstado == "false" ? false : null;
        // TODO: Cursos      = await _cursoService.ObtenerAsync(Busqueda, activo, PaginaActual, TamañoPagina);
        // TODO: TotalCursos = await _cursoService.ContarAsync(Busqueda, activo);

        // Datos de ejemplo
        Cursos = new List<CursoResponse>
        {
            new() { Id = Guid.NewGuid(), Sigla = "CS-101", Nombre = "Introducción a las Ciencias Computacionales", Creditos = 4, Escuela = "Ingeniería y Tecnología", Activo = true  },
            new() { Id = Guid.NewGuid(), Sigla = "BIO-204", Nombre = "Biología Molecular Esencial",                Creditos = 3, Escuela = "Ciencias de la Vida",     Activo = true  },
            new() { Id = Guid.NewGuid(), Sigla = "HIS-110", Nombre = "Historia Global: Siglo XVIII",               Creditos = 3, Escuela = "Humanidades",             Activo = false },
            new() { Id = Guid.NewGuid(), Sigla = "DES-302", Nombre = "Diseño de Interfaces Interactivas",          Creditos = 4, Escuela = "Arte y Diseño",           Activo = true  },
        };
        TotalCursos = 11;
    }
}
