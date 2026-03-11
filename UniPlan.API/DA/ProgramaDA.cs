using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace DA
{
    public class ProgramaDA : IProgramaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProgramaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<ProgramaResponse>> ObtenerProgramas()
        {
            string query = @"ObtenerProgramas";

            var resultadoConsulta = await _sqlConnection.QueryAsync<ProgramaResponse>(
                query,
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }
    }
}