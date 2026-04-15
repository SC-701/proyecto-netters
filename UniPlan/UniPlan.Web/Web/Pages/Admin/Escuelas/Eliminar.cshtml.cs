using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Admin.Escuelas;

[Authorize]
public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public EliminarModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public EscuelaResponse Escuela { get; set; } = default!;

    public async Task<ActionResult> OnGet(Guid? id)
    {
        if (id == null || id == Guid.Empty)
            return NotFound();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "ObtenerEscuela");

        var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.SendAsync(
            new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id))
        );

        respuesta.EnsureSuccessStatusCode();

        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        Escuela = JsonSerializer.Deserialize<EscuelaResponse>(resultado, opciones)!;

        return Page();
    }

    public async Task<ActionResult> OnPost(Guid? id)
    {
        if (id == null || id == Guid.Empty)
            return NotFound();

        if (!ModelState.IsValid)
            return Page();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "EliminarEscuela");

        var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.SendAsync(
            new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id))
        );

        respuesta.EnsureSuccessStatusCode();

        TempData["Exito"] = "Escuela eliminada correctamente.";
        return RedirectToPage("./Index");
    }

    private HttpClient ObtenerClienteConToken()
    {
        var tokenClaim = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Token");

        var cliente = new HttpClient();

        if (tokenClaim != null)
        {
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", tokenClaim.Value);
        }

        return cliente;
    }
}