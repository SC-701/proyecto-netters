

-- ============================================================
--  REQUISITOS
--  SP1: Ver los requisitos de un curso en una carrera
--  SP2: Ver en qué cursos aparece un curso como requisito
-- ============================================================

CREATE   PROCEDURE SP_Requisitos_Agregar
    @IdCarrera         UNIQUEIDENTIFIER,
    @IdCurso           UNIQUEIDENTIFIER,
    @IdCursoRequisito  UNIQUEIDENTIFIER,
    @EsCorequisito     BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Requisitos (IdCarrera, IdCurso, IdCursoRequisito, EsCorequisito, Activo)
    VALUES (@IdCarrera, @IdCurso, @IdCursoRequisito, @EsCorequisito, 1);

    SELECT @IdCarrera AS IdCarrera, @IdCurso AS IdCurso, @IdCursoRequisito AS IdCursoRequisito;
END;