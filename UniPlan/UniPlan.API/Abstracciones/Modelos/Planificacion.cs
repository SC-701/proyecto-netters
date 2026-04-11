using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{


    
        public class Planificacion : PlanificacionBase
        {
            public Guid Id { get; set; }
            public Guid IdUsuario { get; set; }
            public bool Activo { get; set; }
        }
    
    public class PlanificacionBase
    {
        [Required(ErrorMessage = "El período es requerido")]
        [StringLength(50, ErrorMessage = "El período debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "El año es requerido")]
        [RegularExpression(@"(19|20)\d\d", ErrorMessage = "El formato del año no es válido")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(50, ErrorMessage = "El estado debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Estado { get; set; }
    }

    public class PlanificacionRequest : PlanificacionBase
    {
       
    }

    public class PlanificacionResponse : PlanificacionBase
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string Usuario { get; set; }
        public bool Activo { get; set; }
    }
}