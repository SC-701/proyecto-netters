using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Horarios;

[Authorize]
public class EditarModel : PageModel
{

    private readonly IConfiguracion _configuracion;
    public EditarModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public HorarioRequest Input { get; set; } = new();

    public async Task<ActionResult> OnGet()
    {
        if (Id == Guid.Empty)
            return NotFound();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCarrera");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id));
        var respuesta = await cliente.SendAsync(solicitud);
        respuesta.EnsureSuccessStatusCode();
        if (respuesta.StatusCode == HttpStatusCode.OK)
        {

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Input = JsonSerializer.Deserialize<HorarioRequest>(resultado, opciones);

        }

        return Page();
    }
    public async Task<ActionResult> OnPost()
    {

        if (!ModelState.IsValid)
            return Page();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarHorario");
        var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.PutAsJsonAsync<HorarioRequest>(string.Format(endpoint, Id), new HorarioRequest
        {
            Dia = Input.Dia,
            HoraEntrada = Input.HoraEntrada,
            HoraSalida = Input.HoraSalida
        });
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
