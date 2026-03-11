using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

public class CursoFlujo : ICursoFlujo
{
    private readonly ICursoDA _cursoDA;

    public CursoFlujo(ICursoDA cursoDA)
    {
        _cursoDA = cursoDA;
    }

    public async Task<IEnumerable<CursoResponse>> ObtenerCursosPorPrograma(Guid idPrograma)
    {
        return await _cursoDA.ObtenerCursosPorPrograma(idPrograma);
    }
}