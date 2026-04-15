using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Web.Pages.Admin.Cursos;

[Authorize]
public class CrearModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // TODO: cargar desde API (_escuelaService.ObtenerTodosAsync())

    private IConfiguracion _configuracion;
    [BindProperty]
    public CursoRequest curso { get; set; } = default!;
    [BindProperty]
    public List<SelectListItem> escuelas { get; set; } = default!;

    public CrearModel (IConfiguracion configuracion) {
        _configuracion = configuracion;
    }

    public async Task<ActionResult> OnGet() {
        await ObtenerEscuelasAsync();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCurso");
        var cliente = ObtenerClienteConToken();

        var respuesta = await cliente.PostAsJsonAsync(endpoint, curso);
        respuesta.EnsureSuccessStatusCode();
        return RedirectToPage("./Index");
    }

    private async Task ObtenerEscuelasAsync () {
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEscuelas");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        if (respuesta.StatusCode == HttpStatusCode.OK) {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<EscuelaResponse>>(resultado, opciones);
            escuelas = resultadoDeserializado.Select(a =>
                              new SelectListItem {
                                  Value = a.Id.ToString(),
                                  Text = a.Nombre.ToString()
                              }).ToList();
        }
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
