using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Admin.Horarios;

[Authorize]
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
    
    private readonly IConfiguracion _configuracion;

    public IndexModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public IList<HorarioResponse> Horarios { get; set; } = default!;

    public async Task OnGetAsync()
    {
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerHorarios");
        using var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

        var respuesta = await cliente.SendAsync(solicitud);

        if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            Horarios = new List<HorarioResponse>();
            return;
        }

        respuesta.EnsureSuccessStatusCode();

        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        if (!string.IsNullOrWhiteSpace(resultado))
        {
            Horarios = JsonSerializer.Deserialize<List<HorarioResponse>>(resultado, opciones) ?? new List<HorarioResponse>();
        }
        else
        {
            Horarios = new List<HorarioResponse>();
        }
    }

    private HttpClient ObtenerClienteConToken()
    {
        var tokenClaim = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Token");
        var cliente = new HttpClient();
        if (tokenClaim != null)
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", tokenClaim.Value);
        return cliente;
    }
}
