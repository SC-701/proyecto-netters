using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Escuelas;

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
    public int TotalEscuelas { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalEscuelas / TamañoPagina);
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalEscuelas);

    public List<EscuelaResponse> Escuelas { get; set; } = new();

    public void OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        // TODO: bool? activo = FiltroEstado == "true" ? true : FiltroEstado == "false" ? false : null;
        // TODO: Escuelas      = await _escuelaService.ObtenerAsync(Busqueda, activo, PaginaActual, TamañoPagina);
        // TODO: TotalEscuelas = await _escuelaService.ContarAsync(Busqueda, activo);

        // Datos de ejemplo
        Escuelas = new List<EscuelaResponse>
        {
            new() { Id = Guid.NewGuid(), Nombre = "Escuela de Ingeniería",          Area = "Ciencia y Tecnología", Activo = true  },
            new() { Id = Guid.NewGuid(), Nombre = "Academia de Bellas Artes",        Area = "Humanidades",          Activo = true  },
            new() { Id = Guid.NewGuid(), Nombre = "Derecho y Ciencias Políticas",    Area = "Ciencias Sociales",    Activo = false },
            new() { Id = Guid.NewGuid(), Nombre = "Facultad de Medicina",            Area = "Ciencias de la Salud", Activo = false },
        };
        TotalEscuelas = 11;
    }
}
