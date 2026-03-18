
/* ---------- Planificación ---------- */
CREATE   PROCEDURE dbo.CrearPlanificacion
    @IdUsuario UNIQUEIDENTIFIER,
    @NumeroPeriodo INT,
    @NombrePeriodo VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.Usuario
        WHERE Id = @IdUsuario
          AND Estado = 1
    )
    BEGIN
        THROW 50020, 'El usuario indicado no existe o se encuentra inactivo.', 1;
    END;

    IF EXISTS (
        SELECT 1
        FROM dbo.Planificacion
        WHERE IdUsuario = @IdUsuario
          AND NumeroPeriodo = @NumeroPeriodo
          AND Estado = 1
    )
    BEGIN
        THROW 50021, 'Ya existe una planificación activa para ese período.', 1;
    END;

    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.Planificacion
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