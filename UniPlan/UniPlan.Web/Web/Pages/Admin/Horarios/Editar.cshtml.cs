using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Horarios;

public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public HorarioRequest Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: var horario = await _horarioService.ObtenerPorIdAsync(Id);
        // if (horario is null) return NotFound();
        // Input.Dia         = horario.Dia;
        // Input.HoraEntrada = horario.HoraEntrada;
        // Input.HoraSalida  = horario.HoraSalida;

        // Datos de ejemplo
        Input = new HorarioRequest { Dia = "Lunes", HoraEntrada = 8, HoraSalida = 10 };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _horarioService.ActualizarAsync(Id, Input);

        TempData["Exito"] = "Horario actualizado correctamente.";
        return RedirectToPage("/Admin/Horarios/Index");
    }
}
