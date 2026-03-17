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

        public async Task<Guid> CrearPlanificacion(PlanificacionRequest planificacion)
        {
            string query = @"CrearPlanificacion";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(
                query,
                new
                {
                    IdUsuario = planificacion.IdUsuario,
                    NumeroPeriodo = planificacion.NumeroPeriodo,
                    NombrePeriodo = planificacion.NombrePeriodo
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario(Guid idUsuario)
        {
            string query = @"ObtenerPlanificacionesPorUsuario";

            var resultadoConsulta = await _sqlConnection.QueryAsync<PlanificacionResponse>(
                query,
                new
                {
                    IdUsuario = idUsuario
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }

        public async Task AgregarCursoPlanificado(CursoPlanificadoRequest cursoPlanificado)
        {
            string query = @"AgregarCursoPlanificado";

            await _sqlConnection.ExecuteAsync(
                query,
                new
                {
                    IdPlanificacion = cursoPlanificado.IdPlanificacion,
                    IdCurso = cursoPlanificado.IdCurso,
                    IdGrupoHorario = cursoPlanificado.IdGrupoHorario,
                    ColorHex = cursoPlanificado.ColorHex
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario(Guid idUsuario)
        {
            string query = @"ObtenerHorarioUsuario";

            var resultadoConsulta = await _sqlConnection.QueryAsync<HorarioUsuarioResponse>(
                query,
                new
                {
                    IdUsuario = idUsuario
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }
    }
}