using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class EscuelaBase
    {
        [Required(ErrorMessage = "El nombre de la escuela es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener entre 2 y 150 caracteres", MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El área de la escuela es requerida")]
        [StringLength(150, ErrorMessage = "El área debe tener entre 2 y 150 caracteres", MinimumLength = 2)]
        public string Area { get; set; }
    }

    public class EscuelaRequest : EscuelaBase { }

    public class EscuelaResponse : EscuelaBase
    {
        public Guid Id { get; set; }
        public bool Activo { get; set; }
    }
}
