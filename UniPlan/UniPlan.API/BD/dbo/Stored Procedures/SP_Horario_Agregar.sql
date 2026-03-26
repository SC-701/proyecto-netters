

-- ============================================================
--  HORARIO
-- ============================================================

CREATE   PROCEDURE SP_Horario_Agregar
    @HoraEntrada INT,
    @HoraSalida  INT,
    @Dia         NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Horario (Id, HoraEntrada, HoraSalida, Dia, Activo)
    VALUES (@Id, @HoraEntrada, @HoraSalida, @Dia, 1);

    SELECT Id FROM Horario WHERE Id = @Id;
END;