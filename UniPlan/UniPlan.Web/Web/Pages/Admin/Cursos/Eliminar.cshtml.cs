using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Cursos;

[Authorize]
public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public CursoResponse Curso { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Curso = await _cursoService.ObtenerPorIdAsync(Id);
        // if (Curso is null) return NotFound();

        // Datos de ejemplo
        Curso = new CursoResponse
        {
            Id = Id,
            Sigla = "CS-102",
            Nombre = "Estructuras de Datos Avanzadas",
            Creditos = 4,
            Escuela = "Ingeniería y Tecnología",
            Activo = true,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: await _cursoService.EliminarAsync(Id);

        TempData["Exito"] = "Curso eliminado correctamente.";
        return RedirectToPage("/Admin/Cursos/Index");
    }
}
