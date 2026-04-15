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
    public int PaginaInicio => (PaginaActual - 1) * TamañoPagina + 1;
    public int PaginaFin => Math.Min(PaginaActual * TamañoPagina, TotalCursos);

    private IConfiguracion _configuracion;
    public IList<CursoResponse> cursos { get; set; } = default!;

    public IndexModel(IConfiguracion configuracion) {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        PaginaActual = Math.Max(1, PaginaActual);

        // TODO: bool? activo = FiltroEstado == "true" ? true : FiltroEstado == "false" ? false : null;
        // TODO: Cursos      = await _cursoService.ObtenerAsync(Busqueda, activo, PaginaActual, TamañoPagina);
        // TODO: TotalCursos = await _cursoService.ContarAsync(Busqueda, activo);

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCursos");
        using var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        if (respuesta.StatusCode == HttpStatusCode.OK) {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            cursos = JsonSerializer.Deserialize<List<CursoResponse>>(resultado, opciones);
        }



        TotalCursos = 11;
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
