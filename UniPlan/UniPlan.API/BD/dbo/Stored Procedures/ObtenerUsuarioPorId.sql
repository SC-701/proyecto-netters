
CREATE   PROCEDURE dbo.ObtenerUsuarioPorId
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        u.Id,
        u.NombreCompleto,
        u.Correo,
        u.IdPrograma,
        u.Estado,
        u.FechaCreacion
    FROM dbo.Usuario u
    WHERE u.Id = @IdUsuario
      AND u.Estado = 1;
END;