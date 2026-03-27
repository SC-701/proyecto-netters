
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Carrera_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre, Activo
    FROM Carrera
    ORDER BY Nombre;
END;