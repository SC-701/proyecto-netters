using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class CursoDA : ICursoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CursoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<CursoResponse>> ObtenerCursosPorPrograma (Guid idPrograma) {
            const string query = @"ObtenerCursosPorPrograma";

            return await _sqlConnection.QueryAsync<CursoResponse>(
                query,
                new { IdPrograma = idPrograma },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<CursoResponse?> ObtenerCursoPorId (Guid idCurso) {
            const string query = @"ObtenerCursoPorId";

            var resultado = await _sqlConnection.QueryAsync<CursoResponse>(
                query,
                new { IdCurso = idCurso },
                commandType: CommandType.StoredProcedure);

            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<RequisitoCursoResponse>> ObtenerRequisitosPorCurso (Guid idCurso) {
            const string query = @"ObtenerRequisitosPorCurso";

            return await _sqlConnection.QueryAsync<RequisitoCursoResponse>(
                query,
                new { IdCurso = idCurso },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<GrupoHorarioResponse>> ObtenerGruposHorarioPorCurso (Guid idCurso) {
            const string query = @"ObtenerGruposHorarioPorCurso";

            return await _sqlConnection.QueryAsync<GrupoHorarioResponse>(
                query,
                new { IdCurso = idCurso },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<BloqueHorarioResponse>> ObtenerBloquesHorarioGrupo (Guid idGrupoHorario) {
            const string query = @"ObtenerBloquesHorarioGrupo";

            return await _sqlConnection.QueryAsync<BloqueHorarioResponse>(
                query,
                new { IdGrupoHorario = idGrupoHorario },
                commandType: CommandType.StoredProcedure);
        }
    }
}