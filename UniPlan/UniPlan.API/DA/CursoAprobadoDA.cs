using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class CursoAprobadoDA : ICursoAprobadoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CursoAprobadoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task RegistrarCursoAprobado(CursoAprobadoRequest cursoAprobado)
        {
            const string query = @"RegistrarCursoAprobado";

            await _sqlConnection.ExecuteAsync(
                query,
                new
                {
                    IdUsuario = cursoAprobado.IdUsuario,
                    IdCurso = cursoAprobado.IdCurso
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CursoAprobadoResponse>> ObtenerCursosAprobadosPorUsuario(Guid idUsuario)
        {
            const string query = @"ObtenerCursosAprobadosPorUsuario";

            return await _sqlConnection.QueryAsync<CursoAprobadoResponse>(
                query,
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }
    }
}
