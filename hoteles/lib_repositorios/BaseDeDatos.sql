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

CREATE TABLE [Ciudades]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Departamento] INT NOT NULL,
	CONSTRAINT [PK_Ciudades] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Ciudades__Departamentos] FOREIGN KEY ([Departamento]) REFERENCES [Departamentos] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Personas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[TipoDocumento] INT NOT NULL,
	[Documento] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(200) NOT NULL,
	[FechaNacimiento] SMALLDATETIME NOT NULL,
	[Celular] NVARCHAR(50) NOT NULL,
	[Genero] INT NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
	[Contraseña] NVARCHAR(100) NOT NULL,
	[Confirmar] BIT NOT NULL,
	[Restablecer] BIT NOT NULL,
	[Token] NVARCHAR(100) NULL,
	[Ciudad] INT NOT NULL,
	[Activo] BIT NOT NULL,
	CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Personas__TipoDocumentos] FOREIGN KEY ([TipoDocumento]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Personas__Generos] FOREIGN KEY ([Genero]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Personas__Ciudades] FOREIGN KEY ([Ciudad]) REFERENCES [Ciudades] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Empleados]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Persona] INT NOT NULL,
	[Cargo] INT NOT NULL,
	[ARL] INT NOT NULL,
	[Pension] INT NOT NULL,
	[EPS] INT NOT NULL,
	[TipoSangre] INT NOT NULL,
	[EstadoCivil] INT NOT NULL,
	CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Empleados__Persona] FOREIGN KEY ([Persona]) REFERENCES [Personas] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Empleados__Cargo] FOREIGN KEY ([Cargo]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Empleados__ARL] FOREIGN KEY ([ARL]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Empleados__Pension] FOREIGN KEY ([Pension]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Empleados__EPS] FOREIGN KEY ([EPS]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Empleados__TipoSangre] FOREIGN KEY ([TipoSangre]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Empleados__EstadoCivil] FOREIGN KEY ([EstadoCivil]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Proveedores]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[TipoDocumento] INT NOT NULL,
	[Documento] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(200) NOT NULL,
	[Celular] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Ciudad] INT NOT NULL,
	CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Proveedores__TipoDocumentos] FOREIGN KEY ([TipoDocumento]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Proveedores__Ciudades] FOREIGN KEY ([Ciudad]) REFERENCES [Ciudades] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Facturas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Numero] NVARCHAR(35) NOT NULL,
	[Persona] INT NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL,
	[Total] DECIMAL(10,2) NOT NULL,
	[MetodoPago] INT NOT NULL,
	[Tipo] INT NOT NULL,
	[Activo] BIT NOT NULL,
	CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Facturas__Persona] FOREIGN KEY ([Persona]) REFERENCES [Personas] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Facturas__MetodoPago] FOREIGN KEY ([MetodoPago]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Facturas__Tipo] FOREIGN KEY ([Tipo]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Productos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Codigo] NVARCHAR(35) NOT NULL,
	[Nombre] NVARCHAR(35) NOT NULL,
	[Valor] DECIMAL(10,2) NOT NULL,
	[Costo] DECIMAL(10,2) NOT NULL,
	[Cantidad] DECIMAL(10,2) NOT NULL,
	[FechaIngreso] SMALLDATETIME NOT NULL,
	[FechaVencimiento] SMALLDATETIME NULL,
	[Lote] NVARCHAR(35) NULL,
	[Categoria] INT NOT NULL,
	[Proveedor] INT NOT NULL,
	CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Productos__Categoria] FOREIGN KEY ([Categoria]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Productos__Proveedor] FOREIGN KEY ([Proveedor]) REFERENCES [Proveedores] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Detalles]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Factura] INT NOT NULL,
	[Producto] INT NOT NULL,
	[Valor] DECIMAL(10,2) NOT NULL,
	[Cantidad] DECIMAL(10,2) NOT NULL,
	[Total] DECIMAL(10,2) NOT NULL,
	CONSTRAINT [PK_Detalles] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Detalles__Factura] FOREIGN KEY ([Factura]) REFERENCES [Facturas] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Detalles__Producto] FOREIGN KEY ([Producto]) REFERENCES [Productos] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Reserva')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Reserva', 'TiposFacturas', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Reserva')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Facturas', 'TiposFacturas', 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Cedula')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Cedula', 'TipoDocumentos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Cedula Extranjera')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Cedula Extranjera', 'TipoDocumentos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Cedula Extranjera')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('NIT', 'TipoDocumentos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Masculino')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Masculino', 'Generos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Femenino')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Femenino', 'Generos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Administrador')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Administrador', 'Cargo', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Recepcionista')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Recepcionista', 'Cargo', 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Vigilante')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Vigilante', 'Cargo', 2);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Positiva')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Positiva', 'ARL', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Sura')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Sura', 'ARL', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Colpensiones')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Colpensiones', 'Pension', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Protección')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Protección', 'Pension', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Sura')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Sura', 'EPS', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Savia')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Savia', 'EPS', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'O+')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('O+', 'TipoSangre', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'O-')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('O-', 'TipoSangre', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Casado')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Casado', 'EstadoCivil', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Soltero')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Soltero', 'EstadoCivil', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Tarjeta')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Tarjeta', 'MetodoPago', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Efectivo')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Efectivo', 'MetodoPago', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Habitación')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Habitación', 'Categoría', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Comestibles')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Comestibles', 'Categoría', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Paises] WHERE [Nombre] = 'Colombia')
BEGIN
	INSERT INTO [Paises] ([Nombre])
	VALUES ('Colombia');
END 

DECLARE @pais INT
IF NOT EXISTS (SELECT 1 FROM [Departamentos] WHERE [Nombre] = 'Antioquia')
BEGIN
	SET @pais = (SELECT [Id] FROM [Paises] WHERE [Nombre] = 'Colombia');

	INSERT INTO [Departamentos] ([Nombre], [Pais])
	VALUES ('Antioquia', @pais);
END

IF NOT EXISTS (SELECT 1 FROM [Departamentos] WHERE [Nombre] = 'Cundinamarca')
BEGIN
	SET @pais = (SELECT [Id] FROM [Paises] WHERE [Nombre] = 'Colombia');

	INSERT INTO [Departamentos] ([Nombre], [Pais])
	VALUES ('Cundinamarca', @pais);
END

DECLARE @departamento INT
IF NOT EXISTS (SELECT 1 FROM [Ciudades] WHERE [Nombre] = 'Medellin')
BEGIN
	SET @departamento = (SELECT [Id] FROM [Departamentos] WHERE [Nombre] = 'Antioquia');

	INSERT INTO [Ciudades] ([Nombre], [Departamento])
	VALUES ('Medellin', @departamento);
END

DECLARE @ciudad INT
DECLARE @tipo_documento INT
DECLARE @genero INT
IF NOT EXISTS (SELECT 1 FROM [Personas] WHERE [Documento] = '4561321')
BEGIN
	SET @ciudad = (SELECT [Id] FROM [Ciudades] WHERE [Nombre] = 'Medellin');
	SET @tipo_documento = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Cedula');
	SET @genero = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Masculino');

	INSERT INTO [Personas] (
		[TipoDocumento],[Documento],[Nombre],[FechaNacimiento],
		[Celular],[Genero],[Direccion],[Email],[Contraseña],
		[Confirmar],[Restablecer],[Token],[Ciudad],[Activo])
	VALUES (
		@tipo_documento, '4561321', 'Juan Esteban Rios',GETDATE(), 
		'3004567852',@genero, 'Cra 75 # 24 - 16', 'juan@email.com',
		'R3PkoE6RJmq6Q42lhfKzmQ==', 1, 0, NULL, @ciudad, 1);
	-- GJugugs54s56df
END

IF NOT EXISTS (SELECT 1 FROM [Personas] WHERE [Documento] = '134588')
BEGIN
	SET @ciudad = (SELECT [Id] FROM [Ciudades] WHERE [Nombre] = 'Medellin');
	SET @tipo_documento = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Cedula');
	SET @genero = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Masculino');

	INSERT INTO [Personas] (
		[TipoDocumento],[Documento],[Nombre],[FechaNacimiento],
		[Celular],[Genero],[Direccion],[Email],[Contraseña],
		[Confirmar],[Restablecer],[Token],[Ciudad],[Activo])
	VALUES (
		@tipo_documento, '134588', 'Persona de prueba',GETDATE(), 
		'3004567852',@genero, 'Cra 75 # 24 - 16', 'test@email.com',
		'Wya7ZA1yjymnqyhiS1/FmQ==', 1, 0, NULL, @ciudad, 1);
	-- dfg654Iiu76877
END

DECLARE @persona INT
DECLARE @cargo INT
DECLARE @arl INT
DECLARE @pension INT
DECLARE @eps INT
DECLARE @tipoSangre INT
DECLARE @estadoCivil INT

SET @persona = (SELECT [Id] FROM [Personas] WHERE [Documento] = '4561321');
IF NOT EXISTS (SELECT 1 FROM [Empleados] WHERE [Persona] = @persona)
BEGIN
	SET @cargo = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Administrador');
	SET @arl = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Positiva');
	SET @pension = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Colpensiones');
	SET @eps = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Sura');
	SET @tipoSangre = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'O+');
	SET @estadoCivil = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Casado');

	INSERT INTO [Empleados] (
		[Persona],[Cargo],[ARL],[Pension],[EPS],[TipoSangre],[EstadoCivil])
	VALUES (
		@persona, @cargo,@arl,@pension,@eps,@tipoSangre,@estadoCivil);
END

IF NOT EXISTS (SELECT 1 FROM [Proveedores] WHERE [Documento] = '21467814')
BEGIN
	SET @ciudad = (SELECT [Id] FROM [Ciudades] WHERE [Nombre] = 'Medellin');
	SET @tipo_documento = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Cedula');

	INSERT INTO [Proveedores] (
		[TipoDocumento],[Documento],[Nombre],
		[Celular],[Email],[Direccion],[Ciudad])
	VALUES (
		@tipo_documento, '21467814', 'Distri Hoteles','30423906634', 
		'distrihotel@email.com', 'Cl 75 # 92 - 16', @ciudad);
END

DECLARE @metodopago INT
DECLARE @tipo INT

SET @persona = (SELECT [Id] FROM [Personas] WHERE [Documento] = '4561321');
IF NOT EXISTS (SELECT 1 FROM [Facturas] WHERE [Persona] = @persona)
BEGIN
	SET @metodopago = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Efectivo');
	SET @tipo = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Reserva');

	INSERT INTO [Facturas] (
		[Numero],[Persona],[Fecha],[Total],
		[MetodoPago],[Tipo],[Activo])
	VALUES (
		'F0254', @persona, GETDATE(), 25.20,
		@metodopago, @tipo, 1);
END

DECLARE @proveedor INT
DECLARE @categoria INT
IF NOT EXISTS (SELECT 1 FROM [Productos] WHERE [Codigo] = '74917')
BEGIN
	SET @proveedor = (SELECT [Id] FROM [Proveedores] WHERE [Nombre] = 'Distri Hoteles');
	SET @categoria = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Comestibles');

	INSERT INTO [Productos] (
		[Codigo],[Nombre],[Valor],[Costo],
		[Cantidad],[FechaIngreso],[FechaVencimiento],
		[Lote],[Categoria],[Proveedor])
	VALUES (
		'74917', 'Doritos', 2200.22, 3000,22,GETDATE(),
		GETDATE(), 'L41244LK', @categoria,@proveedor);

END

INSERT INTO [Productos] (
		[Codigo],[Nombre],[Valor],[Costo],
		[Cantidad],[FechaIngreso],[FechaVencimiento],
		[Lote],[Categoria],[Proveedor])
	VALUES (
		'48465', 'Margarita de pollo', 2200.22, 3000,22,GETDATE(),
		GETDATE(), 'FA4689A', 21,1);


DECLARE @producto INT
DECLARE @factura INT
IF NOT EXISTS (SELECT 1 FROM [Facturas] WHERE [Numero] = '484898')
BEGIN
	SET @producto = (SELECT [Id] FROM [Productos] WHERE [Nombre] = 'Doritos');
	SET @factura = (SELECT [Id] FROM [Facturas] WHERE [Numero] = 'F0254');

	INSERT INTO [Detalles] (
		[Factura],[Producto],[Valor],
		[Cantidad],[Total])
	VALUES (
		@factura, @producto, 50000.05, 5, 200000.20);

END

SELECT * FROM Paises;
SELECT * FROM Departamentos;
SELECT * FROM Ciudades;
SELECT * FROM Tipos;
SELECT * FROM Personas;
SELECT * FROM Empleados;
SELECT * FROM Proveedores;
SELECT * FROM Facturas;
SELECT * FROM Detalles;
