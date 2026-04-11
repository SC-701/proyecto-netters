using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Cursos;

public class CrearModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty]
    public CursoRequest Input { get; set; } = new();

    // TODO: cargar desde API (_escuelaService.ObtenerTodosAsync())
    public List<SelectListItem> EscuelaOptions { get; set; } = new()
    {
        new("Ingeniería y Ciencias",  Guid.NewGuid().ToString()),
        new("Artes y Humanidades",    Guid.NewGuid().ToString()),
        new("Negocios y Economía",    Guid.NewGuid().ToString()),
    };

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _cursoService.CrearAsync(Input);

        TempData["Exito"] = "Curso creado correctamente.";
        return RedirectToPage("/Admin/Cursos/Index");
    }
}
