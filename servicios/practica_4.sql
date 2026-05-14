CREATE DATABASE practica_4;
GO
USE practica_4;
GO

CREATE TABLE [Jaulas] (
    [Id]     INT IDENTITY(1,1) NOT NULL,
    [Nombre] NVARCHAR(100)     NOT NULL,
    [Estado] INT               NOT NULL,
    CONSTRAINT PK_Jaulas PRIMARY KEY ([Id])
);

CREATE TABLE [Entradas] (
    [Id]                INT             IDENTITY(1,1) NOT NULL,
    [Nombre]            NVARCHAR(100)   NOT NULL,
    [PrecioEntrada]     DECIMAL(18,2)   NOT NULL,
    [Descuento]         DECIMAL(18,4)   NOT NULL,
    [ValorSinDescuento] DECIMAL(18,2)   NOT NULL,
    [Fecha]             DATETIME        NOT NULL,
    [JaulaId]           INT             NOT NULL,
    [Estado]            INT             NOT NULL,
    CONSTRAINT PK_Entradas PRIMARY KEY ([Id]),
    CONSTRAINT FK_Entradas_Jaulas FOREIGN KEY ([JaulaId]) REFERENCES [Jaulas]([Id])
);

CREATE TABLE [Auditorias] (
    [Id]     INT IDENTITY(1,1) NOT NULL,
    [Accion] NVARCHAR(100)     NOT NULL,
    [Fecha]  DATETIME          NOT NULL,
    CONSTRAINT PK_Auditorias PRIMARY KEY ([Id])
);
GO


INSERT INTO Jaulas (Nombre, Estado) VALUES ('Africa - Sabana',     1);
INSERT INTO Jaulas (Nombre, Estado) VALUES ('Africa - Selva',      1);
INSERT INTO Jaulas (Nombre, Estado) VALUES ('Asia - Tigres',       1);
INSERT INTO Jaulas (Nombre, Estado) VALUES ('America - Reptiles',  1);
INSERT INTO Jaulas (Nombre, Estado) VALUES ('Oceania - Aves',      1);
GO
