
CREATE   PROCEDURE dbo.ObtenerCursoPorId
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        c.Id,
        c.Codigo,
        c.Nombre,
        c.Creditos,
        c.Cuatrimestre,
        c.IdPrograma,
        c.Estado,
        c.FechaCreacion
    FROM dbo.Curso c
    WHERE c.Id = @IdCurso;
END;