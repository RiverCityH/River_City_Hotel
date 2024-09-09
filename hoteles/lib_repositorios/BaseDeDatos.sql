﻿CREATE DATABASE db_hoteles
GO
USE db_hoteles;
GO

CREATE TABLE [Paises]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Paises] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [Tipos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Tabla] NVARCHAR(50) NOT NULL,
	[Accion] INT NOT NULL,
	CONSTRAINT [PK_Tipos] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [Departamentos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Pais] INT NOT NULL,
	CONSTRAINT [PK_Departamentos] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Departamentos__Paises] FOREIGN KEY ([Pais]) REFERENCES [Paises] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO