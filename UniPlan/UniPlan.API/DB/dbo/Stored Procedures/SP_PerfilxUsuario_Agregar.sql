CREATE PROCEDURE SP_PerfilxUsuario_Agregar
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPerfil INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM PerfilesxUsuario
        WHERE IdUsuario = @IdUsuario
          AND IdPerfil = @IdPerfil
    )
    BEGIN
        INSERT INTO PerfilesxUsuario (IdUsuario, IdPerfil)
        VALUES (@IdUsuario, @IdPerfil);
    END
END;
