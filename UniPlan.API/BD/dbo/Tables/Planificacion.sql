CREATE TABLE [dbo].[Planificacion] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [IdUsuario]     UNIQUEIDENTIFIER NOT NULL,
    [NumeroPeriodo] INT              NOT NULL,
    [NombrePeriodo] VARCHAR (50)     NULL,
    [FechaCreacion] DATETIME         DEFAULT (getdate()) NOT NULL,
    [Estado]        BIT              DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Planificacion_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([Id])
);

