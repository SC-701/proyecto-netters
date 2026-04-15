using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class RequisitosFlujo : IRequisitosFlujo
    {
        private IRequisitosDA _requisitosDA;

        public RequisitosFlujo(IRequisitosDA requisitosDA)
        {
            _requisitosDA = requisitosDA;
        }
        public async Task<IEnumerable<RequisitosResponse>> Obtener()
        {
            return await _requisitosDA.Obtener();
        }

        public async Task<RequisitosKeyResponse> Agregar(RequisitosRequest requisito)
        {
            return await _requisitosDA.Agregar(requisito);
        }

        public async Task<RequisitosKeyResponse> Editar(RequisitosRequest requisito)
        {
            return await _requisitosDA.Editar(requisito);
        }

        public async Task<RequisitosKeyResponse> Eliminar(RequisitosEliminarRequest requisito)
        {
            return await _requisitosDA.Eliminar(requisito);
        }

        public async Task<IEnumerable<RequisitosResponse>> ObtenerPorCurso(Guid IdCarrera, Guid IdCurso)
        {
            return await _requisitosDA.ObtenerPorCurso(IdCarrera, IdCurso);
        }

        public async Task<IEnumerable<RequisitosResponse>> ObtenerCursosQueLoRequieren(Guid IdCursoRequisito)
        {
            return await _requisitosDA.ObtenerCursosQueLoRequieren(IdCursoRequisito);
        }

        public async Task<RequisitosKeyResponse> CambiarEstado(RequisitosEstadoRequest requisito)
        {
            return await _requisitosDA.CambiarEstado(requisito);
        }
    }
}
