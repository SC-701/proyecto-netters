using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Carreras;

public class IndexModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Filtros bindeados desde query string
    [BindProperty(SupportsGet = true)]
    public string? Busqueda { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FiltroEstado { get; set; }

    // Paginación
    [BindProperty(SupportsGet = true)]
    public int PaginaActual { get; set; } = 1;
    public int TamañoPagina { get; set; } = 10;
    public int TotalCarreras { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalCarreras / TamañoPagina);
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalCarreras);

    // Lista de carreras — se trabaja con CarreraResponse
    public List<CarreraResponse> Carreras { get; set; } = new();

    public void OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        // TODO: reemplazar con llamada real a tu servicio/API
        // bool? activo = FiltroEstado == "true" ? true : FiltroEstado == "false" ? false : null;
        // Carreras      = await _carreraService.ObtenerAsync(Busqueda, activo, PaginaActual, TamañoPagina);
        // TotalCarreras = await _carreraService.ContarAsync(Busqueda, activo);

        // Datos de ejemplo
        Carreras = new List<CarreraResponse>
        {
            new() { Id = Guid.NewGuid(), Nombre = "Arquitectura y Urbanismo",      Activo = true  },
            new() { Id = Guid.NewGuid(), Nombre = "Ingeniería de Sistemas",         Activo = true  },
            new() { Id = Guid.NewGuid(), Nombre = "Derecho y Ciencias Políticas",   Activo = false },
            new() { Id = Guid.NewGuid(), Nombre = "Psicología Organizacional",      Activo = true  },
        };
        TotalCarreras = 11;
    }
}
