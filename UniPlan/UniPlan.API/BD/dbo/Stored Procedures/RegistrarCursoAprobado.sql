CREATE PROCEDURE RegistrarCursoAprobado
    @IdUsuario UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM CursoAprobado
        WHERE IdUsuario = @IdUsuario
          AND IdCurso = @IdCurso
    )
    BEGIN
        RAISERROR('Ese curso ya fue registrado como aprobado.', 16, 1);
        RETURN;
    END

    INSERT INTO CursoAprobado
    (
        Id,
        IdUsuario,
        IdCurso,
        FechaAprobacion
    )
    VALUES
    (
        NEWID(),
        @IdUsuario,
        @IdCurso,
        GETDATE()
    );
END;