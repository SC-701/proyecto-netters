using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Admin.Carreras;

[Authorize]
public class EliminarModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public EliminarModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public CarreraResponse carrera { get; set; } = default;
    public async Task<ActionResult> OnGet(Guid? id)
    {
        if (id == Guid.Empty)
            return NotFound();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCarrera");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        carrera = JsonSerializer.Deserialize<CarreraResponse>(resultado, opciones);
        return Page();
    }

    public async Task<ActionResult> OnPost(Guid? id)
    {
        if (id == Guid.Empty)
            return NotFound();
        if (!ModelState.IsValid)
            return Page();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCarrera");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        return RedirectToPage("./Index");

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
