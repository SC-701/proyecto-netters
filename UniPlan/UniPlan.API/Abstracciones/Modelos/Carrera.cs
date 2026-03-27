using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class CarreraBase
    {
        [Required(ErrorMessage = "El nombre de la carrera es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener entre 2 y 150 caracteres", MinimumLength = 2)]
        public string Nombre { get; set; }
    }

    public class CarreraRequest : CarreraBase { }

    public class CarreraResponse : CarreraBase
    {
        public Guid Id { get; set; }
        public bool Activo { get; set; }
    }
}
