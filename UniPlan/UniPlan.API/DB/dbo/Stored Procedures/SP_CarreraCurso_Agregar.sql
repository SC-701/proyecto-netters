

-- ============================================================
--  CARRERA CURSO
--  ObtenerPorId = todos los cursos de una carrera
-- ============================================================

CREATE   PROCEDURE SP_CarreraCurso_Agregar
    @IdCarrera   UNIQUEIDENTIFIER,
    @IdCurso     UNIQUEIDENTIFIER,
    @Cuatrimestre INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CarreraCurso (IdCarrera, IdCurso, Cuatrimestre, Activo)
    VALUES (@IdCarrera, @IdCurso, @Cuatrimestre, 1);

    SELECT @IdCarrera AS IdCarrera, @IdCurso AS IdCurso;
END;