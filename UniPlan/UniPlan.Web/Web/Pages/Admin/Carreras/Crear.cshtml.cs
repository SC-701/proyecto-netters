using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Web.Pages.Admin.Carreras;

public class CrearModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public CrearModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }
    [BindProperty]
    public CarreraRequest carrera { get; set; }

    public async Task<ActionResult> OnPost()
    {

        if (!ModelState.IsValid)
            return Page();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCarrera");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);
        var respuesta = await cliente.PostAsJsonAsync(endpoint, carrera);
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
