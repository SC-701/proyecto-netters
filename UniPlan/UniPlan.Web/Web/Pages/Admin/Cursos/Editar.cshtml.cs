using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Cursos;

public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    private IConfiguracion _configuracion;

    [BindProperty]
    public CursoRequest curso { get; set; } = default!;
    public List<SelectListItem> escuelas { get; set; } = default!;

    public EditarModel (IConfiguracion configuracion) {
        _configuracion = configuracion;
    }

    public async Task<IActionResult> OnGet()
    {
        if (Id == Guid.Empty)
            return NotFound();
        
        await ObtenerEscuelasAsync();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCurso");
        var cliente = ObtenerClienteConToken();

        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id));
        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();

        if (respuesta.StatusCode == HttpStatusCode.OK) {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var cursoRespuesta = JsonSerializer.Deserialize<CursoResponse>(resultado, opciones);

            if (cursoRespuesta != null) {
                curso = new CursoRequest {
                    Sigla = cursoRespuesta.Sigla,
                    Nombre = cursoRespuesta.Nombre,
                    Creditos = cursoRespuesta.Creditos,
                    IdEscuela = cursoRespuesta.IdEscuela
                };
            }
        }
        return Page();
    }

    public async Task<ActionResult> OnPost () {
        if (Id == Guid.Empty)
            return NotFound();

        if (!ModelState.IsValid) {
            await ObtenerEscuelasAsync();
            return Page();
        }

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCurso");
        var cliente = ObtenerClienteConToken();

        var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Id.ToString()), curso);
        if (!respuesta.IsSuccessStatusCode) {
            await ObtenerEscuelasAsync();
            var error = await respuesta.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Error al guardar: {error}");
            return Page();
        }
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
