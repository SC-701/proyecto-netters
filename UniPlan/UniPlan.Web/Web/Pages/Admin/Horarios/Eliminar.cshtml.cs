using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Horarios;

public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public HorarioResponse Horario { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Horario = await _horarioService.ObtenerPorIdAsync(Id);
        // if (Horario is null) return NotFound();

        // Datos de ejemplo
        Horario = new HorarioResponse
        {
            Id = Id,
            Dia = "Lunes",
            HoraEntrada = 8,
            HoraSalida = 10,
            Activo = true,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: await _horarioService.EliminarAsync(Id);

        TempData["Exito"] = "Horario eliminado correctamente.";
        return RedirectToPage("/Admin/Horarios/Index");
    }
}
