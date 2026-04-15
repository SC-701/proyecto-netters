using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
    public int TotalPaginas => TotalEscuelas == 0 ? 1 : (int)Math.Ceiling((double)TotalEscuelas / TamañoPagina);
    public int PaginaInicio => TotalEscuelas == 0 ? 0 : (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalEscuelas);

    private readonly IConfiguracion _configuracion;

    public IList<EscuelaResponse> Escuelas { get; set; } = new List<EscuelaResponse>();

    public IndexModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        await CargarEscuelas();
    }

    public async Task<IActionResult> OnPostActivarAsync(Guid id, string? busqueda, string? filtroEstado, int paginaActual = 1)
    {
        if (id == Guid.Empty)
            return NotFound();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "ActivarEscuela");

        using var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.PutAsync(string.Format(endpoint, id), null);

        respuesta.EnsureSuccessStatusCode();

        TempData["Exito"] = "Escuela activada correctamente.";

        return RedirectToPage("./Index", new
        {
            Busqueda = busqueda,
            FiltroEstado = filtroEstado,
            PaginaActual = paginaActual
        });
    }

    public async Task<IActionResult> OnPostInactivarAsync(Guid id, string? busqueda, string? filtroEstado, int paginaActual = 1)
    {
        if (id == Guid.Empty)
            return NotFound();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "EliminarEscuela");

        using var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.SendAsync(
            new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id))
        );

        respuesta.EnsureSuccessStatusCode();

        TempData["Exito"] = "Escuela inactivada correctamente.";

        return RedirectToPage("./Index", new
        {
            Busqueda = busqueda,
            FiltroEstado = filtroEstado,
            PaginaActual = paginaActual
        });
    }

    private async Task CargarEscuelas()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "ObtenerEscuelas");

        using var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

        var respuesta = await cliente.SendAsync(solicitud);

        if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            Escuelas = new List<EscuelaResponse>();
            TotalEscuelas = 0;
            return;
        }

        respuesta.EnsureSuccessStatusCode();

        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var todasLasEscuelas = !string.IsNullOrWhiteSpace(resultado)
            ? JsonSerializer.Deserialize<List<EscuelaResponse>>(resultado, opciones) ?? new List<EscuelaResponse>()
            : new List<EscuelaResponse>();

        if (!string.IsNullOrWhiteSpace(Busqueda))
        {
            todasLasEscuelas = todasLasEscuelas
                .Where(e =>
                    (!string.IsNullOrWhiteSpace(e.Nombre) && e.Nombre.Contains(Busqueda, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(e.Area) && e.Area.Contains(Busqueda, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(FiltroEstado))
        {
            bool? activo = FiltroEstado.ToLower() switch
            {
                "true" => true,
                "false" => false,
                _ => null
            };

            if (activo.HasValue)
            {
                todasLasEscuelas = todasLasEscuelas
                    .Where(e => e.Activo == activo.Value)
                    .ToList();
            }
        }

        TotalEscuelas = todasLasEscuelas.Count;

        Escuelas = todasLasEscuelas
            .Skip((PaginaActual - 1) * TamañoPagina)
            .Take(TamañoPagina)
            .ToList();
    }

    private HttpClient ObtenerClienteConToken()
    {
        var tokenClaim = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Token");

        var cliente = new HttpClient();

        if (tokenClaim != null)
        {
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenClaim.Value);
        }

        return cliente;
    }
}