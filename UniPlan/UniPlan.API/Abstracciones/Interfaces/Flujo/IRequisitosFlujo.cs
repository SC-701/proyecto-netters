using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IRequisitosFlujo
    {
        Task<IEnumerable<RequisitosResponse>> ObtenerPorCurso(Guid IdCarrera, Guid IdCurso);
        Task<IEnumerable<RequisitosResponse>> ObtenerCursosQueLoRequieren(Guid IdCursoRequisito);
        Task<IEnumerable<RequisitosResponse>> Obtener();
        Task<RequisitosKeyResponse> Agregar(RequisitosRequest requisito);
        Task<RequisitosKeyResponse> Editar(RequisitosRequest requisito);
        Task<RequisitosKeyResponse> Eliminar(RequisitosEliminarRequest requisito);

        Task<RequisitosKeyResponse> CambiarEstado(RequisitosEstadoRequest requisito);
    }
}
