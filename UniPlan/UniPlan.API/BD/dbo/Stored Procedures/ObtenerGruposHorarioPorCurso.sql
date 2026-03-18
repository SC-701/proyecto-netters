
CREATE   PROCEDURE dbo.ObtenerGruposHorarioPorCurso
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        gh.Id,
        gh.IdCurso,
        gh.NumeroGrupo,
        gh.Profesor,
        gh.Cupo,
        gh.Estado
    FROM dbo.GrupoHorario gh
    WHERE gh.IdCurso = @IdCurso
    ORDER BY gh.NumeroGrupo;
END;