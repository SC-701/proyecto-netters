using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Admin.Horarios;

[Authorize]
public class EliminarModel : PageModel
{
    private readonly IConfiguracion _configuracion;

    public EliminarModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public HorarioResponse Horario { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id == Guid.Empty)
            return NotFound();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerHorario");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id));

        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Horario = JsonSerializer.Deserialize<HorarioResponse>(resultado, opciones);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Id == Guid.Empty)
            return NotFound();
        if (!ModelState.IsValid)
            return Page();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarHorario");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, Id));
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
