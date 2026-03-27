CREATE PROCEDURE SP_Perfil_Agregar
    @Nombre NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id INT;

    SELECT @Id = ISNULL(MAX(Id), 0) + 1
    FROM Perfiles;

    INSERT INTO Perfiles
    (
        Id,
        Nombre,
        FechaCreacion,
        FechaModificacion,
        UsuarioCrea,
        UsuarioModifica
    )
    VALUES
    (
        @Id,
        @Nombre,
        GETDATE(),
        NULL,
        NULL,
        NULL
    );

    SELECT Id
    FROM Perfiles
    WHERE Id = @Id;
END;
