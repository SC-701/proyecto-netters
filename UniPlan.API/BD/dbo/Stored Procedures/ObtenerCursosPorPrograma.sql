CREATE PROCEDURE ObtenerCursosPorPrograma
    @IdPrograma UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Codigo,
        Nombre,
        Creditos,
        Cuatrimestre,
        IdPrograma,
        Estado,
        FechaCreacion
    FROM Curso
    WHERE IdPrograma = @IdPrograma
      AND Estado = 1
    ORDER BY Cuatrimestre, Nombre;
END;