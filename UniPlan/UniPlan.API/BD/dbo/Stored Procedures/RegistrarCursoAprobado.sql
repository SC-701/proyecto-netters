
/* ---------- Cursos aprobados ---------- */
CREATE   PROCEDURE dbo.RegistrarCursoAprobado
    @IdUsuario UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.CursoAprobado
        WHERE IdUsuario = @IdUsuario
          AND IdCurso = @IdCurso
    )
    BEGIN
        THROW 50040, 'Ese curso ya fue registrado como aprobado.', 1;
    END;

    INSERT INTO dbo.CursoAprobado
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