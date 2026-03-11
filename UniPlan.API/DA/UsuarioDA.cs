using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {

        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;


        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        public async Task<UsuarioResponse?> IniciarSesion(string correo, string contrasenna)
        {
            string query = @"IniciarSesion";

            var resultadoConsulta = await _sqlConnection.QueryAsync<UsuarioResponse>(
                query,
                new
                {
                    Correo = correo,
                    Contrasenna = contrasenna
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta.FirstOrDefault();
        }



        public async Task<Guid> RegistrarUsuario(UsuarioRequest usuario)
        {
            string query = @"RegistrarUsuario";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(
                query,
                new
                {
              
                    NombreCompleto = usuario.NombreCompleto,
                    Correo = usuario.Correo,
                    Contrasenna = usuario.Contrasenna,
                    IdPrograma = usuario.IdPrograma
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }
    }
}
