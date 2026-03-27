

-- ============================================================
--  CURSO PLANIFICACION
--  ObtenerPorId = todos los cursos de una planificación
-- ============================================================

CREATE   PROCEDURE SP_CursoPlanificacion_Agregar
    @IdPlan   UNIQUEIDENTIFIER,
    @IdCurso  UNIQUEIDENTIFIER,
    @Estado   NVARCHAR(50),
    @IdHorario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CursoPlanificacion (IdPlan, IdCurso, Estado, IdHorario, Activo)
    VALUES (@IdPlan, @IdCurso, @Estado, @IdHorario, 1);

    SELECT @IdPlan AS IdPlan, @IdCurso AS IdCurso;
END;