using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Carreras;

public class EditarModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CarreraRequest Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: var carrera = await _carreraService.ObtenerPorIdAsync(Id);
        // if (carrera is null) return NotFound();
        // Input.Nombre = carrera.Nombre;

        // Datos de ejemplo
        Input = new CarreraRequest { Nombre = "Ingeniería en Sistemas Computacionales" };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _carreraService.ActualizarAsync(Id, Input);

        TempData["Exito"] = "Carrera actualizada correctamente.";
        return RedirectToPage("/Admin/Carreras/Index");
    }
}
