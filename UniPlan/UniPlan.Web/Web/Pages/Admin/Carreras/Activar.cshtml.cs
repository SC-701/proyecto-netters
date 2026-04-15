using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Carreras
{
    public class ActivarModel : PageModel
    {

        public string AdminName { get; set; } = "Admin User";
        public string AdminEmail { get; set; } = "admin@uniplan.edu";

        private readonly IConfiguracion _configuracion;

        public ActivarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public CarreraResponse carrera { get; set; } = default;
        public void OnGet()
        {
        }
    }
}
