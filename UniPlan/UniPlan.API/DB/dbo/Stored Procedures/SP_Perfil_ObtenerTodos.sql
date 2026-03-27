CREATE PROCEDURE SP_Perfil_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre
    FROM Perfiles
    ORDER BY Nombre;
END;