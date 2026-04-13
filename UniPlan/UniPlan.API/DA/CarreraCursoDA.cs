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
    public class CarreraCursoDA : ICarreraCursoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor

        public CarreraCursoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
        public async Task<Guid> Agregar(CarreraCursoRequest carreraCurso)
        {
            string query = @"SP_CarreraCurso_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdCarrera = carreraCurso.IdCarrera,
                IdCurso = carreraCurso.IdCurso,
                Cuatrimestre = carreraCurso.Cuatrimestre,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid IdCarrera, Guid IdCurso, CarreraCursoRequest carreraCurso)
        {
            await verificarCarreraExiste(IdCarrera);
            string query = @"SP_CarreraCurso_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdCarrera = IdCarrera,
                IdCurso = IdCurso,
                Cuatrimestre = carreraCurso.Cuatrimestre,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid IdCarrera, Guid IdCurso)
        {
            await verificarCarreraExiste(IdCarrera);
            string query = @"SP_CarreraCurso_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdCarrera = IdCarrera,
                IdCurso = IdCurso
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<CarreraCursoResponse>> Obtener()
        {
            string query = @"SP_CarreraCurso_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CarreraCursoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CarreraCursoDetalle> Obtener(Guid IdCarrera)
        {
            string query = @"SP_CarreraCurso_ObtenerPorIdCarrera";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CarreraCursoDetalle>(query,
                new
                {
                    IdCarrera = IdCarrera
                });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarCarreraExiste(Guid IdCarrera)
        {
            CarreraCursoResponse? resultadoConsultaCarreraCurso = await Obtener(IdCarrera);
            if (resultadoConsultaCarreraCurso == null)
                throw new Exception("No se encontro la relacion CarreraCurso");
        }
        #endregion
    }
}

