using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ICursoDA
    {
        Task<IEnumerable<CursoResponse>> ObtenerCursosPorPrograma (Guid idPrograma);
        Task<CursoResponse?> ObtenerCursoPorId (Guid idCurso);
        Task<IEnumerable<RequisitoCursoResponse>> ObtenerRequisitosPorCurso (Guid idCurso);
        Task<IEnumerable<GrupoHorarioResponse>> ObtenerGruposHorarioPorCurso (Guid idCurso);
        Task<IEnumerable<BloqueHorarioResponse>> ObtenerBloquesHorarioGrupo (Guid idGrupoHorario);

    }
}
