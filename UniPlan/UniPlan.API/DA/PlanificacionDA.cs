using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class PlanificacionDA : IPlanificacionDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public PlanificacionDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> AgregarPlanificacion(Planificacion planificacion)
        {
            string sql = "SP_Planificacion_Agregar";

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sql,
                new
                {
                    planificacion.Periodo,
                    planificacion.Anio,
                    planificacion.IdUsuario,
                    planificacion.Estado
                },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificaciones()
        {
            string sql = "SP_Planificacion_ObtenerTodos";

            var resultado = await _sqlConnection.QueryAsync<PlanificacionResponse>(
                sql,
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuarioId(Guid idUsuario)
        {
            string sql = "SP_Planificacion_ObtenerPorIdUsuario";

            var resultado = await _sqlConnection.QueryAsync<PlanificacionResponse>(
                sql,
                new
                {
                    IdUsuario = idUsuario
                },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<Guid> EditarPlanificacion(Guid id, Planificacion planificacion)
        {
            string sql = "SP_Planificacion_Actualizar";

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sql,
                new
                {
                    Id = id,
                    planificacion.Periodo,
                    planificacion.Anio,
                    planificacion.Estado
                },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<Guid> EliminarPlanificacion(Guid id)
        {
            string sql = "SP_Planificacion_Eliminar";

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sql,
                new
                {
                    Id = id
                },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }
    }
}