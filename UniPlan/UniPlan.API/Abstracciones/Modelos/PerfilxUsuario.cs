using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class PerfilxUsuarioBase
    {
        [Required(ErrorMessage = "El ID del usuario es requerido")]
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "El ID del perfil es requerido")]
        public int IdPerfil { get; set; }
    }

    public class PerfilxUsuarioRequest : PerfilxUsuarioBase { }

    public class PerfilxUsuarioResponse : PerfilxUsuarioBase { }
}
