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

        [Required(ErrorMessage = "El estado activo es requerido")]
        public bool Activo { get; set; }
    }

    public class CarreraCursoRequest : CarreraCursoBase
    {
    }

    public class CarreraCursoResponse : CarreraCursoBase
    {
        public string Carrera { get; set; }
        public string Curso { get; set; }
    }

    public class CarreraCursoDetalle : CarreraCursoResponse
    {
        public int Creditos { get; set; }
        public string Sigla { get; set; }
    }
}