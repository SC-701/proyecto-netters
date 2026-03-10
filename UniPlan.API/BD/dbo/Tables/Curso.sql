CREATE TABLE [dbo].[Curso] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Codigo]        VARCHAR (20)     NOT NULL,
    [Nombre]        VARCHAR (150)    NOT NULL,
    [Creditos]      INT              NOT NULL,
    [Cuatrimestre]  INT              NOT NULL,
    [IdPrograma]    UNIQUEIDENTIFIER NOT NULL,
    [Estado]        BIT              DEFAULT ((1)) NOT NULL,
    [FechaCreacion] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Curso_Programa] FOREIGN KEY ([IdPrograma]) REFERENCES [dbo].[Programa] ([IdPrograma])
);

