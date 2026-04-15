using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class RequisitosDA : IRequisitosDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public RequisitosDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD

        public async Task<RequisitosKeyResponse> Agregar(RequisitosRequest requisito)
        {
            string query = @"SP_Requisitos_Agregar";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosKeyResponse>(query, new
            {
                IdCarrera = requisito.IdCarrera,
                IdCurso = requisito.IdCurso,
                IdCursoRequisito = requisito.IdCursoRequisito,
                EsCorequisito = requisito.EsCorequisito
            });

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<RequisitosKeyResponse> Editar(RequisitosRequest requisito)
        {
            await verificarRequisitoExiste(requisito.IdCarrera, requisito.IdCurso, requisito.IdCursoRequisito);

            string query = @"SP_Requisitos_Actualizar";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosKeyResponse>(query, new
            {
                IdCarrera = requisito.IdCarrera,
                IdCurso = requisito.IdCurso,
                IdCursoRequisito = requisito.IdCursoRequisito,
                EsCorequisito = requisito.EsCorequisito
            });

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<RequisitosKeyResponse> Eliminar(RequisitosEliminarRequest requisito)
        {
            await verificarRequisitoExiste(requisito.IdCarrera, requisito.IdCurso, requisito.IdCursoRequisito);

            string query = @"SP_Requisitos_Eliminar";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosKeyResponse>(query, new
            {
                IdCarrera = requisito.IdCarrera,
                IdCurso = requisito.IdCurso,
                IdCursoRequisito = requisito.IdCursoRequisito
            });

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<IEnumerable<RequisitosResponse>> ObtenerPorCurso(Guid IdCarrera, Guid IdCurso)
        {
            string query = @"SP_Requisitos_ObtenerPorCurso";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosResponse>(query, new
            {
                IdCarrera = IdCarrera,
                IdCurso = IdCurso
            });

            return resultadoConsulta;
        }

        public async Task<IEnumerable<RequisitosResponse>> ObtenerCursosQueLoRequieren(Guid IdCursoRequisito)
        {
            string query = @"SP_Requisitos_ObtenerCursosQueLoRequieren";
            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosResponse>(query, new
            {
                IdCursoRequisito = IdCursoRequisito
            });

            return resultadoConsulta;
        }

        #endregion

        #region Helpers

        private async Task verificarRequisitoExiste(Guid IdCarrera, Guid IdCurso, Guid IdCursoRequisito)
        {
            IEnumerable<RequisitosResponse> resultadoConsulta = await ObtenerPorCurso(IdCarrera, IdCurso);

            bool existe = resultadoConsulta.Any(x =>
                x.IdCarrera == IdCarrera &&
                x.IdCurso == IdCurso &&
                x.IdCursoRequisito == IdCursoRequisito);

            if (!existe)
            {
                throw new Exception("El requisito no existe");
            }
        }

        public async Task<RequisitosKeyResponse> CambiarEstado(RequisitosEstadoRequest requisito)
        {
            string query = @"SP_Requisitos_CambiarEstado";

            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosKeyResponse>(
                query,
                new
                {
                    IdCarrera = requisito.IdCarrera,
                    IdCurso = requisito.IdCurso,
                    IdCursoRequisito = requisito.IdCursoRequisito,
                    Activo = requisito.Activo
                },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<IEnumerable<RequisitosResponse>> Obtener()
        {
            string query = @"SP_Requisitos_ObtenerTodos";

            var resultadoConsulta = await _sqlConnection.QueryAsync<RequisitosResponse>(
                query,
                commandType: System.Data.CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }

        #endregion
    }
}