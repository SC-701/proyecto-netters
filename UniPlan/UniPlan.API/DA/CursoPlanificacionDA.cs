using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA {
    public class CursoPlanificacionDA : ICursoPlanificacionDA {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CursoPlanificacionDA (IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task<Guid> Agregar (CursoPlanificacionRequest cursoplanificacion) {
            string query = @"SP_CursoPlanificacion_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                IdPlan = cursoplanificacion.IdPlan,
                IdCurso = cursoplanificacion.IdCurso,
                Estado = cursoplanificacion.Estado,
                IdHorario = cursoplanificacion.IdHorario
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar (Guid IdPlan, Guid IdCurso, CursoPlanificacionRequest cursoplanificacion) {
            await verificarCursoPlanificacionExiste(IdPlan, IdCurso);
            string query = @"SP_CursoPlanificacion_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                IdPlan = IdPlan,
                IdCurso = IdCurso,
                Estado = cursoplanificacion.Estado,
                IdHorario = cursoplanificacion.IdHorario
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar (Guid IdPlan, Guid IdCurso) {
            await verificarCursoPlanificacionExiste(IdPlan, IdCurso);
            string query = @"SP_CursoPlanificacion_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                IdPlan = IdPlan,
                IdCurso = IdCurso
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<CursoPlanificacionResponse>> Obtener () {
            string query = @"SP_CursoPlanificacion_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoPlanificacionResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CursoPlanificacionResponse> Obtener (Guid IdPlan) {
            string query = @"SP_CursoPlanificacion_ObtenerPorIdPlan";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoPlanificacionResponse>(query, new {
                IdPlan = IdPlan
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<CursoPlanificacionResponse> ObtenerUno (Guid IdPlan, Guid IdCurso) {
            string query = @"SP_CursoPlanificacion_ObtenerUno";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoPlanificacionResponse>(query, new {
                IdPlan = IdPlan,
                IdCurso = IdCurso
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarCursoPlanificacionExiste (Guid IdPlan, Guid IdCurso) {
            CursoPlanificacionResponse? resultadoConsulta = await ObtenerUno(IdPlan, IdCurso);
            if (resultadoConsulta == null) {
                throw new Exception("El curso buscado en la planificación no existe");
            }
        }
        #endregion
    }
}
