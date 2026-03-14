CREATE PROCEDURE IniciarSesion
    @Correo VARCHAR(150),
    @Contrasenna VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        Id,
        NombreCompleto,
        Correo,
        IdPrograma,
        Estado,
        FechaCreacion
    FROM Usuario
    WHERE Correo = @Correo
      AND Contrasenna = @Contrasenna
      AND Estado = 1;
END;