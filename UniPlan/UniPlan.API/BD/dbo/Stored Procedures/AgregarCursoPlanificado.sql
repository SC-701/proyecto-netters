
CREATE   PROCEDURE dbo.AgregarCursoPlanificado
    @IdPlanificacion UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER,
    @IdGrupoHorario UNIQUEIDENTIFIER,
    @ColorHex VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.CursoPlanificado
        WHERE IdPlanificacion = @IdPlanificacion
          AND IdCurso = @IdCurso
    )
    BEGIN
        THROW 50030, 'El curso ya fue agregado a la planificación.', 1;
    END;

    INSERT INTO dbo.CursoPlanificado
    (
        Id,
        IdPlanificacion,
        IdCurso,
        IdGrupoHorario,
        ColorHex
    )
    VALUES
    (
        NEWID(),
        @IdPlanificacion,
        @IdCurso,
        @IdGrupoHorario,
        @ColorHex
    );
END;