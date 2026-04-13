using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Admin.Carreras;

public class IndexModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Filtros bindeados desde query string
    [BindProperty(SupportsGet = true)]
    public string? Busqueda { get; set; }

    //[BindProperty(SupportsGet = true)]
   public string? FiltroEstado { get; set; }

    // Paginación
    [BindProperty(SupportsGet = true)]
    public int PaginaActual { get; set; } = 1;
    public int TamañoPagina { get; set; } = 10;
    public int TotalCarreras { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalCarreras / TamañoPagina);
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalCarreras);

    private readonly IConfiguracion _configuracion;
    public IList<CarreraResponse> carreras { get; set; } = default!;

    public IndexModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCarreras");
        using var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        carreras = JsonSerializer.Deserialize<List<CarreraResponse>>(resultado, opciones);
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
