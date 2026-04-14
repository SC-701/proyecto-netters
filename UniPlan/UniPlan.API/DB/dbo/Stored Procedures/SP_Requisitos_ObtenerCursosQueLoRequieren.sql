CREATE   PROCEDURE [dbo].[SP_Requisitos_ObtenerCursosQueLoRequieren]
    @IdCursoRequisito UNIQUEIDENTIFIER
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
    WHERE r.IdCursoRequisito = @IdCursoRequisito
    ORDER BY ca.Nombre, cu.Sigla;
END;