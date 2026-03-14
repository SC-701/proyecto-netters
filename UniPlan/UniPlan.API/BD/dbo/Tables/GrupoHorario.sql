CREATE TABLE [dbo].[GrupoHorario] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [IdCurso]     UNIQUEIDENTIFIER NOT NULL,
    [NumeroGrupo] VARCHAR (20)     NOT NULL,
    [Profesor]    VARCHAR (150)    NULL,
    [Cupo]        INT              NULL,
    [Estado]      BIT              DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GrupoHorario_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id])
);

