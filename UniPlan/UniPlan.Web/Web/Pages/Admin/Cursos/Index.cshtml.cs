using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

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
    public int PaginaInicio => TotalCursos == 0 ? 0 : (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => TotalCursos == 0 ? 0 : Math.Min(PaginaActual * TamañoPagina, TotalCursos);

    private IConfiguracion _configuracion;
    public IList<CursoResponse> cursos { get; set; } = default!;

    public IndexModel(IConfiguracion configuracion) {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCursos");
        using var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
   
        // Para buscar
        var listaCursos = new List<CursoResponse>();

        if (respuesta.StatusCode == HttpStatusCode.OK) {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            listaCursos = JsonSerializer.Deserialize<List<CursoResponse>>(resultado, opciones);
        }

        // Filtro por nombre
        if (!string.IsNullOrWhiteSpace(Busqueda)) {
            listaCursos = listaCursos
                .Where(c => !string.IsNullOrWhiteSpace(c.Nombre) &&
                            c.Nombre.Contains(Busqueda, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Filtro por estado
        if (!string.IsNullOrWhiteSpace(FiltroEstado)) {
            if (bool.TryParse(FiltroEstado, out bool activo)) {
                listaCursos = listaCursos
                    .Where(c => c.Activo == activo)
                    .ToList();
            }
        }

        TotalCursos = listaCursos.Count;

        if (TotalPaginas > 0 && PaginaActual > TotalPaginas)
            PaginaActual = TotalPaginas;

        cursos = listaCursos
            .Skip((PaginaActual - 1) * TamañoPagina)
            .Take(TamañoPagina)
            .ToList();
    }

    // Helper — extrae el JWT de los claims y configura el HttpClient
    private HttpClient ObtenerClienteConToken () {
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
