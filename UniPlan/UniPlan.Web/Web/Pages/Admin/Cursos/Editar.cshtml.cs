using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Cursos;

[Authorize]
public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CursoRequest Input { get; set; } = new();

    // TODO: cargar desde API
    public List<SelectListItem> EscuelaOptions { get; set; } = new()
    {
        new("Ingeniería y Ciencias", Guid.NewGuid().ToString()),
        new("Artes y Humanidades",   Guid.NewGuid().ToString()),
        new("Negocios y Economía",   Guid.NewGuid().ToString()),
    };

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: var curso = await _cursoService.ObtenerPorIdAsync(Id);
        // if (curso is null) return NotFound();
        // Input = new CursoRequest { Sigla = curso.Sigla, Nombre = curso.Nombre, Creditos = curso.Creditos, IdEscuela = curso.IdEscuela };

        // Datos de ejemplo
        Input = new CursoRequest
        {
            Sigla = "CS-101",
            Nombre = "Introducción a las Ciencias Computacionales",
            Creditos = 4,
            IdEscuela = Guid.Empty,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _cursoService.ActualizarAsync(Id, Input);

        TempData["Exito"] = "Curso actualizado correctamente.";
        return RedirectToPage("/Admin/Cursos/Index");
    }
}
