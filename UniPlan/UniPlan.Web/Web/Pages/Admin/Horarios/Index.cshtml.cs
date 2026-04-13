using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Horarios;

public class IndexModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Filtros
    [BindProperty(SupportsGet = true)]
    public string? Busqueda { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FiltroDia { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FiltroEstado { get; set; }

    // Paginación
    [BindProperty(SupportsGet = true)]
    public int PaginaActual { get; set; } = 1;
    public int TamañoPagina { get; set; } = 10;
    public int TotalHorarios { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalHorarios / TamañoPagina);
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalHorarios);

    public List<HorarioResponse> Horarios { get; set; } = new();

    public void OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        // TODO: bool? activo = FiltroEstado == "true" ? true : FiltroEstado == "false" ? false : null;
        // TODO: Horarios      = await _horarioService.ObtenerAsync(Busqueda, FiltroDia, activo, PaginaActual, TamañoPagina);
        // TODO: TotalHorarios = await _horarioService.ContarAsync(Busqueda, FiltroDia, activo);

        // Datos de ejemplo
        Horarios = new List<HorarioResponse>
        {
            new() { Id = Guid.NewGuid(), Dia = "Lunes",     HoraEntrada = 8,  HoraSalida = 10, Activo = true  },
            new() { Id = Guid.NewGuid(), Dia = "Martes",    HoraEntrada = 11, HoraSalida = 13, Activo = false },
            new() { Id = Guid.NewGuid(), Dia = "Miércoles", HoraEntrada = 14, HoraSalida = 16, Activo = true  },
        };
        TotalHorarios = 11;
    }
}
