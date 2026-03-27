
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Horario_Actualizar
    @Id          UNIQUEIDENTIFIER,
    @HoraEntrada INT,
    @HoraSalida  INT,
    @Dia         NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Horario
    SET HoraEntrada = @HoraEntrada,
        HoraSalida  = @HoraSalida,
        Dia         = @Dia
    WHERE Id = @Id;

    SELECT Id FROM Horario WHERE Id = @Id;
END;