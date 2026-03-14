CREATE PROCEDURE AgregarCursoPlanificado
    @IdPlanificacion UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER,
    @IdGrupoHorario UNIQUEIDENTIFIER = NULL,
    @ColorHex VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM CursoPlanificado
        WHERE IdPlanificacion = @IdPlanificacion
          AND IdCurso = @IdCurso
    )
    BEGIN
        RAISERROR('El curso ya fue agregado a la planificación.', 16, 1);
        RETURN;
    END

    INSERT INTO CursoPlanificado
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