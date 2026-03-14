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

        public async Task<IEnumerable<CursoResponse>> ObtenerCursosPorPrograma(Guid idPrograma)
        {
            string query = @"ObtenerCursosPorPrograma";

            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoResponse>(
                query,
                new
                {
                    IdPrograma = idPrograma
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }
    }
}