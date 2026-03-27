CREATE TABLE [dbo].[Planificacion] (
    [Id]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Periodo]   NVARCHAR (50)    NOT NULL,
    [Anio]      INT              NOT NULL,
    [IdUsuario] UNIQUEIDENTIFIER NOT NULL,
    [Estado]    NVARCHAR (50)    NOT NULL,
    [Activo]    BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Planificacion] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Planificacion_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([Id])
);

