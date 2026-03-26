
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Curso_ObtenerPorSigla
    @Sigla NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  c.Id,
            c.Sigla,
            c.Nombre,
            c.Creditos,
            c.IdEscuela,
            e.Nombre AS Escuela,
            c.Activo
    FROM  Curso   c
    JOIN  Escuela e ON e.Id = c.IdEscuela
    WHERE c.Sigla = @Sigla;
END;