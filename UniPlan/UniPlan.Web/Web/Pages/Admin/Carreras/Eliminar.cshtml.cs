using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Carreras;

public class EliminarModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public CarreraResponse Carrera { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Carrera = await _carreraService.ObtenerPorIdAsync(Id);
        // if (Carrera is null) return NotFound();

        // Datos de ejemplo
        Carrera = new CarreraResponse
        {
            Id = Id,
            Nombre = "Ingeniería en Sistemas Computacionales",
            Activo = true,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: await _carreraService.EliminarAsync(Id);

        TempData["Exito"] = "Carrera eliminada correctamente.";
        return RedirectToPage("/Admin/Carreras/Index");
    }
}
