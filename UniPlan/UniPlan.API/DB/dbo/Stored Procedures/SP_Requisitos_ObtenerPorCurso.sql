CREATE   PROCEDURE [dbo].[SP_Requisitos_ObtenerPorCurso]
    @IdCarrera UNIQUEIDENTIFIER,
    @IdCurso   UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  r.IdCarrera,
            ca.Nombre  AS Carrera,
            r.IdCurso,
            cu.Sigla   AS SiglaCurso,
            cu.Nombre  AS Curso,
            r.IdCursoRequisito,
            cr.Sigla   AS SiglaRequisito,
            cr.Nombre  AS CursoRequisito,
            r.EsCorequisito,
            r.Activo
    FROM  Requisitos r
    JOIN  Carrera    ca ON ca.Id = r.IdCarrera
    JOIN  Curso      cu ON cu.Id = r.IdCurso
    JOIN  Curso      cr ON cr.Id = r.IdCursoRequisito
    WHERE r.IdCarrera = @IdCarrera
      AND r.IdCurso   = @IdCurso
    ORDER BY cr.Sigla;
END;