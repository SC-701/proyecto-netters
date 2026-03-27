CREATE TABLE [dbo].[Horario] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [HoraEntrada] INT              NOT NULL,
    [HoraSalida]  INT              NOT NULL,
    [Dia]         NVARCHAR (20)    NOT NULL,
    [Activo]      BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Horario] PRIMARY KEY CLUSTERED ([Id] ASC)
);

