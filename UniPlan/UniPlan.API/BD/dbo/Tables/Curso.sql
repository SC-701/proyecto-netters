CREATE TABLE [dbo].[Curso] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Sigla]     NVARCHAR (20)    NOT NULL,
    [Nombre]    NVARCHAR (150)   NOT NULL,
    [Creditos]  INT              NOT NULL,
    [IdEscuela] UNIQUEIDENTIFIER NOT NULL,
    [Activo]    BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Curso_Escuela] FOREIGN KEY ([IdEscuela]) REFERENCES [dbo].[Escuela] ([Id])
);

