using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICursoFlujo
    {
        Task<IEnumerable<CursoResponse>> ObtenerCursosPorPrograma (Guid idPrograma);
        Task<CursoDetalleResponse?> ObtenerCursoDetalle (Guid idCurso);
        Task<IEnumerable<CursoDisponibleResponse>> ObtenerCursosDisponiblesParaUsuario (Guid idUsuario);
    }
}
