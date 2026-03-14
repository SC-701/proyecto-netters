CREATE PROCEDURE RegistrarUsuario
    @NombreCompleto VARCHAR(150),
    @Correo VARCHAR(150),
    @Contrasenna VARCHAR(255),
    @IdPrograma UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Usuario
        WHERE Correo = @Correo
    )
    BEGIN
        RAISERROR('El correo ya se encuentra registrado.', 16, 1);
        RETURN;
    END

    INSERT INTO Usuario
    (
        Id,
        NombreCompleto,
        Correo,
        Contrasenna,
        IdPrograma,
        Estado,
        FechaCreacion
    )
    VALUES
    (
        NEWID(),
        @NombreCompleto,
        @Correo,
        @Contrasenna,
        @IdPrograma,
        1,
        GETDATE()
    );
END;