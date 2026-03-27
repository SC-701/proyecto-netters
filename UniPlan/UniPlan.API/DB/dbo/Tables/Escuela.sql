CREATE TABLE [dbo].[Escuela] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre] NVARCHAR (150)   NOT NULL,
    [Area]   NVARCHAR (150)   NOT NULL,
    [Activo] BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Escuela] PRIMARY KEY CLUSTERED ([Id] ASC)
);

