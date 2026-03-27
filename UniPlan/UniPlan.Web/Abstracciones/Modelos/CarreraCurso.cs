using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class CarreraCursoBase
    {
        [Required(ErrorMessage = "El Id de la carrera es requerido")]
        public Guid IdCarrera { get; set; }

        [Required(ErrorMessage = "El Id del curso es requerido")]
        public Guid IdCurso { get; set; }

        [Required(ErrorMessage = "El cuatrimestre es requerido")]
        [Range(1, 12, ErrorMessage = "El cuatrimestre debe estar entre 1 y 12")]
        public int Cuatrimestre { get; set; }
    }

    public class CarreraCursoRequest : CarreraCursoBase { }

    public class CarreraCursoResponse : CarreraCursoBase
    {
        public string Carrera { get; set; }
        public string Curso { get; set; }
        public bool Activo { get; set; }
    }
}
