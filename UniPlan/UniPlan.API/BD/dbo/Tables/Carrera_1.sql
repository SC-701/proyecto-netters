CREATE TABLE [dbo].[Carrera] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre] NVARCHAR (150)   NOT NULL,
    [Activo] BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Carrera] PRIMARY KEY CLUSTERED ([Id] ASC)
);

