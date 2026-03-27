CREATE PROCEDURE SP_PerfilxUsuario_Eliminar
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPerfil INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM PerfilesxUsuario
    WHERE IdUsuario = @IdUsuario
      AND IdPerfil = @IdPerfil;

END;
GO