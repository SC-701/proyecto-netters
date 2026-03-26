using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class RequisitosBase
    {
        [Required(ErrorMessage = "El Id de la carrera es requerido")]
        public Guid IdCarrera { get; set; }

        [Required(ErrorMessage = "El Id del curso es requerido")]
        public Guid IdCurso { get; set; }

        [Required(ErrorMessage = "El Id del curso requisito es requerido")]
        public Guid IdCursoRequisito { get; set; }

        [Required(ErrorMessage = "Debe indicar si es corequisito")]
        public bool EsCorequisito { get; set; }
    }

    public class RequisitosRequest : RequisitosBase { }

    public class RequisitosResponse : RequisitosBase
    {
        public string Carrera { get; set; }
        public string Curso { get; set; }
        public string CursoRequisito { get; set; }
        public bool Activo { get; set; }
    }
}
