using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Escuelas;

[Authorize]
public class CrearModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    [BindProperty]
    public EscuelaRequest Input { get; set; } = new();

    public CrearModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "AgregarEscuela");

        using var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.PostAsJsonAsync(endpoint, Input);

        respuesta.EnsureSuccessStatusCode();

        TempData["Exito"] = "Escuela creada correctamente.";
        return RedirectToPage("/Admin/Escuelas/Index");
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