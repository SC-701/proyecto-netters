using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class PerfilBase
    {
        [Required(ErrorMessage = "El nombre del perfil es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener entre 1 y 150 caracteres", MinimumLength = 1)]
        public string Nombre { get; set; }
    }

    public class PerfilRequest : PerfilBase { }

    public class PerfilResponse : PerfilBase
    {
        public int Id { get; set; }
    }
}
