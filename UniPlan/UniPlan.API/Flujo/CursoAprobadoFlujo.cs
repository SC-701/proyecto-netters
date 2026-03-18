using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class CursoAprobadoFlujo : ICursoAprobadoFlujo
    {
        private readonly ICursoAprobadoDA _cursoAprobadoDA;
        private readonly IUsuarioDA _usuarioDA;
        private readonly ICursoDA _cursoDA;

        public CursoAprobadoFlujo(
            ICursoAprobadoDA cursoAprobadoDA,
            IUsuarioDA usuarioDA,
            ICursoDA cursoDA)
        {
            _cursoAprobadoDA = cursoAprobadoDA;
            _usuarioDA = usuarioDA;
            _cursoDA = cursoDA;
        }

        public async Task RegistrarCursoAprobado(CursoAprobadoRequest cursoAprobado)
        {
            var usuario = await _usuarioDA.ObtenerUsuarioPorId(cursoAprobado.IdUsuario);
            if (usuario == null)
                throw new InvalidOperationException("El usuario indicado no existe.");

            var curso = await _cursoDA.ObtenerCursoPorId(cursoAprobado.IdCurso);
            if (curso == null)
                throw new InvalidOperationException("El curso indicado no existe.");

            await _cursoAprobadoDA.RegistrarCursoAprobado(cursoAprobado);
        }

        public async Task<IEnumerable<CursoAprobadoResponse>> ObtenerCursosAprobadosPorUsuario(Guid idUsuario)
        {
            return await _cursoAprobadoDA.ObtenerCursosAprobadosPorUsuario(idUsuario);
        }
    }
}
