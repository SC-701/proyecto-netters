using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class HorarioDA : IHorarioDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public HorarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task<Guid> Agregar(HorarioRequest horario)
        {
            string query = @"SP_Horario_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                HoraEntrada = horario.HoraEntrada,
                HoraSalida = horario.HoraSalida,
                Dia = horario.Dia
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, HorarioRequest horario)
        {
            await verificarHorarioExiste(Id);
            string query = @"SP_Horario_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                HoraEntrada = horario.HoraEntrada,
                HoraSalida = horario.HoraSalida,
                Dia = horario.Dia
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarHorarioExiste(Id);
            string query = @"SP_Horario_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<HorarioResponse>> Obtener()
        {
            string query = @"SP_Horario_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<HorarioResponse>(query);
            return resultadoConsulta;
        }

        public async Task<HorarioResponse> Obtener(Guid Id)
        {
            string query = @"SP_Horario_ObtenerPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<HorarioResponse>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Activar(Guid Id)
        {
            await verificarHorarioExiste(Id);
            string query = @"SP_Horario_Activar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }
        #endregion

        #region Helpers
        private async Task verificarHorarioExiste(Guid Id)
        {
            HorarioResponse? resultadoConsulta = await Obtener(Id);
            if (resultadoConsulta == null)
            {
                throw new Exception("El horario no existe");
            }
        }
        #endregion
    }
}