using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Admin.Escuelas
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

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public EscuelaResponse Escuela { get; set; } = new();

        public async Task<ActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "ObtenerEscuela");

            var cliente = ObtenerClienteConToken();
            var respuesta = await cliente.SendAsync(
                new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id))
            );

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Escuela = JsonSerializer.Deserialize<EscuelaResponse>(resultado, opciones)!;

            if (Escuela == null)
                return NotFound();

            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (Id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndpoints", "ActivarEscuela");

            var cliente = ObtenerClienteConToken();
            var respuesta = await cliente.PutAsync(string.Format(endpoint, Id), null);

            respuesta.EnsureSuccessStatusCode();

            TempData["Exito"] = "Escuela activada correctamente.";
            return RedirectToPage("./Index");
        }

        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Token");

            var cliente = new HttpClient();

            if (tokenClaim != null)
            {
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            }

            return cliente;
        }
    }
}