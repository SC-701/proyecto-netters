using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Escuelas;

public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public EscuelaResponse Escuela { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Escuela = await _escuelaService.ObtenerPorIdAsync(Id);
        // if (Escuela is null) return NotFound();

        // Datos de ejemplo
        Escuela = new EscuelaResponse
        {
            Id = Id,
            Nombre = "Saint Patrick's Academy",
            Area = "Humanidades y Artes",
            Activo = true,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: await _escuelaService.EliminarAsync(Id);

        TempData["Exito"] = "Escuela eliminada correctamente.";
        return RedirectToPage("/Admin/Escuelas/Index");
    }
}
