
/* ---------- Cursos ---------- */
CREATE   PROCEDURE dbo.ObtenerCursosPorPrograma
    @IdPrograma UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id,
        c.Codigo,
        c.Nombre,
        c.Creditos,
        c.Cuatrimestre,
        c.IdPrograma,
        c.Estado,
        c.FechaCreacion
    FROM dbo.Curso c
    WHERE c.IdPrograma = @IdPrograma
      AND c.Estado = 1
    ORDER BY c.Cuatrimestre, c.Nombre;
END;