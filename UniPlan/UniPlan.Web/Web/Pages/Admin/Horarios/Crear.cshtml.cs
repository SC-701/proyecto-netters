using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Horarios;

public class CrearModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public CrearModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    [BindProperty]
    public HorarioRequest Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarHorario");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);
        var respuesta = await cliente.PostAsJsonAsync(endpoint, Input);
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
