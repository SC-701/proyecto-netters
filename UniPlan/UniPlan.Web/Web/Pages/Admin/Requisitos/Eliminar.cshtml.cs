using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Requisitos;

public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid IdCarrera { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid IdCurso { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid IdCursoRequisito { get; set; }

    public RequisitosResponse Requisito { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Requisito = await _requisitoService.ObtenerAsync(IdCarrera, IdCurso, IdCursoRequisito);
        // if (Requisito is null) return NotFound();

        // Datos de ejemplo
        Requisito = new RequisitosResponse
        {
            IdCarrera = IdCarrera,
            IdCurso = IdCurso,
            IdCursoRequisito = IdCursoRequisito,
            Carrera = "Ingeniería de Sistemas",
            Curso = "Advanced Physics II",
            CursoRequisito = "Calculus III",
            EsCorequisito = false,
            Activo = true,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: await _requisitoService.EliminarAsync(IdCarrera, IdCurso, IdCursoRequisito);

        TempData["Exito"] = "Requisito eliminado correctamente.";
        return RedirectToPage("/Admin/Requisitos/Index");
    }
}
