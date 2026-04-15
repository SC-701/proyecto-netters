using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Escuelas;

public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public EditarModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EscuelaRequest Input { get; set; } = new();

    public async Task<ActionResult> OnGetAsync()
    {
        if (Id == Guid.Empty)
            return NotFound();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "ObtenerEscuela");

        var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.SendAsync(
            new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id))
        );

        respuesta.EnsureSuccessStatusCode();

        if (respuesta.StatusCode == HttpStatusCode.OK)
        {
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var escuela = JsonSerializer.Deserialize<EscuelaResponse>(resultado, opciones);

            if (escuela == null)
                return NotFound();

            Input = new EscuelaRequest
            {
                Nombre = escuela.Nombre,
                Area = escuela.Area
            };
        }

        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (Id == Guid.Empty)
            return NotFound();

        if (!ModelState.IsValid)
            return Page();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "EditarEscuela");

        var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.PutAsJsonAsync(
            string.Format(endpoint, Id),
            new EscuelaRequest
            {
                Nombre = Input.Nombre,
                Area = Input.Area
            });

        respuesta.EnsureSuccessStatusCode();

        TempData["Exito"] = "Escuela actualizada correctamente.";
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