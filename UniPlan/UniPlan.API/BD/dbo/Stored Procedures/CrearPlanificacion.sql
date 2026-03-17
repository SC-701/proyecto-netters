CREATE   PROCEDURE [dbo].[CrearPlanificacion]
    @IdUsuario UNIQUEIDENTIFIER,
    @NumeroPeriodo INT,
    @NombrePeriodo VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

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
        @Id,
        @IdUsuario,
        @NumeroPeriodo,
        @NombrePeriodo,
        GETDATE(),
        1
    );

    SELECT @Id;
END;