using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Cursos;

[Authorize]
public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public CursoResponse curso { get; set; } = default!;
    private IConfiguracion _configuracion;

    public EliminarModel (IConfiguracion configuracion) {
        _configuracion = configuracion;
    }

    public async Task<ActionResult> OnGet () {
        if (Id == Guid.Empty)
            return NotFound();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCurso");
        var cliente = ObtenerClienteConToken();

        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id));
        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        if (respuesta.StatusCode == HttpStatusCode.OK) {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            curso = JsonSerializer.Deserialize<CursoResponse>(resultado, opciones);
        }
        return Page();
    }
    public async Task<ActionResult> OnPost () {
        if (Id == Guid.Empty)
            return NotFound();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCurso");
        var cliente = ObtenerClienteConToken();

        var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, Id));
        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        return RedirectToPage("./Index");
    }

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
