/* ---------- Programas ---------- */
CREATE   PROCEDURE dbo.ObtenerProgramas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.IdPrograma,
        p.Nombre,
        p.Descripcion,
        p.Estado,
        p.FechaCreacion
    FROM dbo.Programa p
    WHERE p.Estado = 1
    ORDER BY p.Nombre;
END;