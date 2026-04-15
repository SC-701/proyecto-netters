using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Carreras;

[Authorize]

public class EditarModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        [BindProperty]
        public CarreraRequest carreraRequest { get; set; }


        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id ==  Guid.Empty)
                return  NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCarrera");
            var cliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                carreraRequest = JsonSerializer.Deserialize<CarreraRequest>(resultado, opciones);

            }
            
            return Page();
        }
        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCarrera");
            var cliente = ObtenerClienteConToken();
            var respuesta = await cliente.PutAsJsonAsync<CarreraRequest>(string.Format(endpoint, carreraRequest.Id), new CarreraRequest
            { 
                Nombre = carreraRequest.Nombre,
                Activo = carreraRequest.Activo
            });
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");

        }

        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Token");
            var cliente = new HttpClient();
            if (tokenClaim != null)
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            return cliente;
        }
}
