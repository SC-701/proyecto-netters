
CREATE   PROCEDURE dbo.ObtenerRequisitosPorCurso
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rc.Id,
        rc.IdCurso,
        rc.IdCursoRequerido,
        rc.EsCorrequisito,
        cReq.Codigo AS CodigoCursoRequerido,
        cReq.Nombre AS NombreCursoRequerido
    FROM dbo.RequisitoCurso rc
    INNER JOIN dbo.Curso cReq
        ON cReq.Id = rc.IdCursoRequerido
    WHERE rc.IdCurso = @IdCurso
    ORDER BY rc.EsCorrequisito, cReq.Codigo, cReq.Nombre;
END;