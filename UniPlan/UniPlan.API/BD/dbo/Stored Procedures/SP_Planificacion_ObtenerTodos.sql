
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Planificacion_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  p.Id,
            p.Periodo,
            p.Anio,
            p.IdUsuario,
            u.NombreUsuario AS Usuario,
            p.Estado,
            p.Activo
    FROM  Planificacion p
    JOIN  Usuarios      u ON u.Id = p.IdUsuario
    ORDER BY p.Anio DESC, p.Periodo;
END;