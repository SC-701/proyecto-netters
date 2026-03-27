-- Eliminación real (no lógica)
CREATE PROCEDURE SP_Perfil_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Perfiles
    WHERE Id = @Id;

    SELECT @Id AS Id;
END;