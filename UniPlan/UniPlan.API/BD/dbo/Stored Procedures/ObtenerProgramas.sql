CREATE PROCEDURE ObtenerProgramas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdPrograma,
        Nombre,
        Descripcion,
        Estado,
        FechaCreacion
    FROM Programa
    WHERE Estado = 1
    ORDER BY Nombre;
END;