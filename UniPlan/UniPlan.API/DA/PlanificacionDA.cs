using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class PlanificacionDA : IPlanificacionDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public PlanificacionDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> CrearPlanificacion (PlanificacionRequest planificacion) {
            const string query = @"CrearPlanificacion";

            return await _sqlConnection.ExecuteScalarAsync<Guid>(
                query,
                new {
                    IdUsuario = planificacion.IdUsuario,
                    NumeroPeriodo = planificacion.NumeroPeriodo,
                    NombrePeriodo = planificacion.NombrePeriodo
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario (Guid idUsuario) {
            const string query = @"ObtenerPlanificacionesPorUsuario";

            return await _sqlConnection.QueryAsync<PlanificacionResponse>(
                query,
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<PlanificacionResponse?> ObtenerPlanificacionPorId (Guid idPlanificacion) {
            const string query = @"ObtenerPlanificacionPorId";

            var resultado = await _sqlConnection.QueryAsync<PlanificacionResponse>(
                query,
                new { IdPlanificacion = idPlanificacion },
                commandType: CommandType.StoredProcedure);

            return resultado.FirstOrDefault();
        }

        public async Task<bool> ExistePlanificacionUsuarioPeriodo (Guid idUsuario, int numeroPeriodo) {
            const string query = @"ExistePlanificacionUsuarioPeriodo";

            return await _sqlConnection.ExecuteScalarAsync<bool>(
                query,
                new {
                    IdUsuario = idUsuario,
                    NumeroPeriodo = numeroPeriodo
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task AgregarCursoPlanificado (CursoPlanificadoRequest cursoPlanificado) {
            const string query = @"AgregarCursoPlanificado";

            await _sqlConnection.ExecuteAsync(
                query,
                new {
                    IdPlanificacion = cursoPlanificado.IdPlanificacion,
                    IdCurso = cursoPlanificado.IdCurso,
                    IdGrupoHorario = cursoPlanificado.IdGrupoHorario,
                    ColorHex = cursoPlanificado.ColorHex
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task EliminarCursoPlanificado (Guid idPlanificacion, Guid idCurso) {
            const string query = @"EliminarCursoPlanificado";

            await _sqlConnection.ExecuteAsync(
                query,
                new {
                    IdPlanificacion = idPlanificacion,
                    IdCurso = idCurso
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CursoPlanificadoHorarioResponse>> ObtenerCursosPlanificadosPorPlanificacion (Guid idPlanificacion) {
            const string query = @"ObtenerCursosPlanificadosPorPlanificacion";

            return await _sqlConnection.QueryAsync<CursoPlanificadoHorarioResponse>(
                query,
                new { IdPlanificacion = idPlanificacion },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario (Guid idUsuario) {
            const string query = @"ObtenerHorarioUsuario";

            return await _sqlConnection.QueryAsync<HorarioUsuarioResponse>(
                query,
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }
    }
}