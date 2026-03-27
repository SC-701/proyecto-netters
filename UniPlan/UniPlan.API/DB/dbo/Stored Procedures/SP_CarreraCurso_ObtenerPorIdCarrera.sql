
-- -----------------------------------------------------------
-- ObtenerPorIdCarrera: todos los cursos de una carrera
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_CarreraCurso_ObtenerPorIdCarrera
    @IdCarrera UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  cc.IdCarrera,
            ca.Nombre  AS Carrera,
            cc.IdCurso,
            cu.Sigla,
            cu.Nombre  AS Curso,
            cu.Creditos,
            cc.Cuatrimestre,
            cc.Activo
    FROM  CarreraCurso cc
    JOIN  Carrera      ca ON ca.Id = cc.IdCarrera
    JOIN  Curso        cu ON cu.Id = cc.IdCurso
    WHERE cc.IdCarrera = @IdCarrera
    ORDER BY cc.Cuatrimestre, cu.Sigla;
END;