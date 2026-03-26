
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Escuela_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre, Area, Activo
    FROM Escuela
    
    ORDER BY Nombre;
END;