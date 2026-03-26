using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class CursoPlanificacionBase
    {
        [Required(ErrorMessage = "El Id del plan es requerido")]
        public Guid IdPlan { get; set; }

        [Required(ErrorMessage = "El Id del curso es requerido")]
        public Guid IdCurso { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(50, ErrorMessage = "El estado debe tener entre 2 y 50 caracteres", MinimumLength = 2)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El Id del horario es requerido")]
        public Guid IdHorario { get; set; }
    }

    public class CursoPlanificacionRequest : CursoPlanificacionBase { }

    public class CursoPlanificacionResponse : CursoPlanificacionBase
    {
        public string Curso { get; set; }
        public string Horario { get; set; }
        public bool Activo { get; set; }
    }
}
