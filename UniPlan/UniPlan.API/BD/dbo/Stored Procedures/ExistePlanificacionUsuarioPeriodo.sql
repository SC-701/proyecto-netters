
CREATE   PROCEDURE dbo.ExistePlanificacionUsuarioPeriodo
    @IdUsuario UNIQUEIDENTIFIER,
    @NumeroPeriodo INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CAST(
        CASE WHEN EXISTS (
            SELECT 1
            FROM dbo.Planificacion
            WHERE IdUsuario = @IdUsuario
              AND NumeroPeriodo = @NumeroPeriodo
              AND Estado = 1
        ) THEN 1 ELSE 0 END
    AS BIT) AS Existe;
END;