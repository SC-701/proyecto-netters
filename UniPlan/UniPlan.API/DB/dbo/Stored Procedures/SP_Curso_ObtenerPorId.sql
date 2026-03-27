
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Curso_ObtenerPorId
    @Id UNIQUEIDENTIFIER
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
    WHERE c.Id = @Id;
END;