using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reglas;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Seguridad;

public class RegisterModel : PageModel
{
    [BindProperty]
    public Usuario usuario { get; set; } = default!;
    private IConfiguracion _configuracion;

    public RegisterModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var hash = Autenticacion.GenerarHash(usuario.Password);
        usuario.PasswordHash = Autenticacion.ObtenerHash(hash);

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "Registro");
        var cliente = new HttpClient();
        var respuesta = await cliente.PostAsJsonAsync<UsuarioBase>(endpoint, usuario);
        respuesta.EnsureSuccessStatusCode();
        return RedirectToPage("/App/Dashboard");
    }
}
