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
    }

    public class RequisitosRequest : RequisitosBase
    {
        [Required(ErrorMessage = "El valor de EsCorequisito es requerido")]
        public bool EsCorequisito { get; set; }
    }

    public class RequisitosEliminarRequest : RequisitosBase
    {
    }

    public class RequisitosKeyResponse : RequisitosBase
    {
    }

    public class RequisitosResponse : RequisitosBase
    {
        public string Carrera { get; set; }
        public string SiglaCurso { get; set; }
        public string Curso { get; set; }
        public string SiglaRequisito { get; set; }
        public string CursoRequisito { get; set; }
        public bool EsCorequisito { get; set; }
        public bool Activo { get; set; }
    }
}
