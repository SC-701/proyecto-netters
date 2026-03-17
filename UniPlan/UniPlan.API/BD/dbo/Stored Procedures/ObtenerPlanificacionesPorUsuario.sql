CREATE   PROCEDURE [dbo].[ObtenerPlanificacionesPorUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        IdUsuario,
        NumeroPeriodo,
        NombrePeriodo,
        FechaCreacion,
        Estado
    FROM Planificacion
    WHERE IdUsuario = @IdUsuario
      AND Estado = 1
    ORDER BY NumeroPeriodo;
END;