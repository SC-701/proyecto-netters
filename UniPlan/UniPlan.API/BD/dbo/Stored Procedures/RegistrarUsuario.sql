
CREATE   PROCEDURE dbo.RegistrarUsuario
    @NombreCompleto VARCHAR(150),
    @Correo VARCHAR(150),
    @Contrasenna VARCHAR(255),
    @IdPrograma UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.Usuario
        WHERE Correo = @Correo
    )
    BEGIN
        THROW 50010, 'El correo ya se encuentra registrado.', 1;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.Programa
        WHERE IdPrograma = @IdPrograma
          AND Estado = 1
    )
    BEGIN
        THROW 50011, 'El programa indicado no existe o se encuentra inactivo.', 1;
    END;

    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.Usuario
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
        @Id,
        @NombreCompleto,
        @Correo,
        @Contrasenna,
        @IdPrograma,
        1,
        GETDATE()
    );

    SELECT @Id;
END;