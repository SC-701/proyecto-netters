using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Escuelas;

public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EscuelaRequest Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: var escuela = await _escuelaService.ObtenerPorIdAsync(Id);
        // if (escuela is null) return NotFound();
        // Input.Nombre = escuela.Nombre;
        // Input.Area   = escuela.Area;

        // Datos de ejemplo
        Input = new EscuelaRequest
        {
            Nombre = "Escuela de Ingeniería",
            Area = "Ciencia y Tecnología",
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _escuelaService.ActualizarAsync(Id, Input);

        TempData["Exito"] = "Escuela actualizada correctamente.";
        return RedirectToPage("/Admin/Escuelas/Index");
    }
}
