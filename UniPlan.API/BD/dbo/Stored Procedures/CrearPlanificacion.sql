CREATE PROCEDURE CrearPlanificacion
    @IdUsuario UNIQUEIDENTIFIER,
    @NumeroPeriodo INT,
    @NombrePeriodo VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Planificacion
    (
        Id,
        IdUsuario,
        NumeroPeriodo,
        NombrePeriodo,
        FechaCreacion,
        Estado
    )
    VALUES
    (
        NEWID(),
        @IdUsuario,
        @NumeroPeriodo,
        @NombrePeriodo,
        GETDATE(),
        1
    );
END;