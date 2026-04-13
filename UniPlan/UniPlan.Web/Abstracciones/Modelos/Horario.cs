using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class HorarioBase
    {
        [Required(ErrorMessage = "La hora de entrada es requerida")]
        [Range(8, 21, ErrorMessage = "La hora de entrada debe estar entre 0 y 23")]
        public int HoraEntrada { get; set; }

        [Required(ErrorMessage = "La hora de salida es requerida")]
        [Range(8, 21, ErrorMessage = "La hora de salida debe estar entre 0 y 23")]
        public int HoraSalida { get; set; }

        [Required(ErrorMessage = "El día es requerido")]
        [RegularExpression(@"^(Lunes|Martes|Miércoles|Jueves|Viernes|Sábado|Domingo)$",
            ErrorMessage = "El día debe ser: Lunes, Martes, Miércoles, Jueves, Viernes, Sábado o Domingo")]
        public string Dia { get; set; }
    }

    public class HorarioRequest : HorarioBase { }

    public class HorarioResponse : HorarioBase
    {
        public Guid Id { get; set; }
        public bool Activo { get; set; }
    }
}
