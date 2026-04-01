using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class CursoBase
    {
        [Required(ErrorMessage = "La sigla del curso es requerida")]
        [StringLength(20, ErrorMessage = "La sigla debe tener entre 2 y 20 caracteres", MinimumLength = 2)]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "El nombre del curso es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener entre 2 y 150 caracteres", MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los créditos del curso son requeridos")]
        [Range(1, 20, ErrorMessage = "Los créditos deben estar entre 1 y 20")]
        public int Creditos { get; set; }
    }

    public class CursoRequest : CursoBase
    {
        [Required(ErrorMessage = "El Id de la escuela es requerido")]
        public Guid IdEscuela { get; set; }
    }

    public class CursoResponse : CursoBase
    {
        public Guid Id { get; set; }
        public Guid IdEscuela { get; set; }
        public string Escuela { get; set; }
        public bool Activo { get; set; }
    }


}
