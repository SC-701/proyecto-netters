
CREATE   PROCEDURE dbo.ObtenerCursosAprobadosPorUsuario
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ca.Id,
        ca.IdUsuario,
        ca.IdCurso,
        ca.FechaAprobacion,
        c.Codigo AS CodigoCurso,
        c.Nombre AS NombreCurso
    FROM dbo.CursoAprobado ca
    INNER JOIN dbo.Curso c
        ON c.Id = ca.IdCurso
    WHERE ca.IdUsuario = @IdUsuario
    ORDER BY ca.FechaAprobacion DESC, c.Codigo, c.Nombre;
END;