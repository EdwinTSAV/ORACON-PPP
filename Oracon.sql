USE [Oracon]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 04/07/2021 20:18:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](25) NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 04/07/2021 20:18:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [nvarchar](50) NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](1000) NULL,
	[Precio] [decimal](18, 2) NULL,
	[IdDocente] [int] NULL,
	[IdCategoria] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Cursos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CursoUsuario]    Script Date: 04/07/2021 20:18:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CursoUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[idCurso] [int] NULL,
	[Estado] [bit] NULL,
	[Pagado] [bit] NULL,
 CONSTRAINT [PK_CursoUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favoritos]    Script Date: 04/07/2021 20:18:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favoritos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[IdCurso] [int] NULL,
 CONSTRAINT [PK_Favoritos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 04/07/2021 20:18:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_DetalleCurso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 04/07/2021 20:18:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [nvarchar](50) NULL,
	[Nombres] [nvarchar](50) NULL,
	[Apellidos] [nvarchar](50) NULL,
	[Celular] [nvarchar](12) NULL,
	[Correo] [nvarchar](50) NULL,
	[Username] [nvarchar](10) NULL,
	[Password] [nvarchar](100) NULL,
	[Recovery] [nvarchar](100) NULL,
	[IdRol] [int] NULL,
	[Twitter] [nvarchar](50) NULL,
	[Facebook] [nvarchar](50) NULL,
	[LinkedIn] [nvarchar](50) NULL,
	[YouTube] [nvarchar](50) NULL,
	[Instagram] [nvarchar](50) NULL,
	[Titulo] [nvarchar](75) NULL,
	[Biografia] [nvarchar](250) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (1, N'Contabilidad')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (2, N'Administracion')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (3, N'Marketing')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (4, N'Ofimatica')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Curso] ON 

INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (1, N'/FilesBD/unnamed.jpg', N'C++', N'Introduccion a la programacion con lenguaje C++', CAST(10.00 AS Decimal(18, 2)), 2011, 1, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (2, N'/project/icono.jpg', N'Boletas', N'Diseño, calculos de las boletas y facturas de ventas', CAST(1455.00 AS Decimal(18, 2)), 2011, 1, 1)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (3, N'/project/icono.jpg', N'Diseño de paginas', N'Diseño de paginas, online, marketing', CAST(0.00 AS Decimal(18, 2)), 2011, 3, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (4, N'/project/icono.jpg', N'Empresas Norte', N'Estudia y aprende, acerca de las empresas, como se administra una, y los registros que conlleva.', CAST(1455.00 AS Decimal(18, 2)), 2011, 2, 1)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (7, N'/project/icono.jpg', N'Perfiles Plantillas', N'Perfiles y plantillas', CAST(100.00 AS Decimal(18, 2)), 1011, 4, 1)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (12, N'/FilesBD/maxresdefault.jpg', N'Hojas', N'Lavar el auto de papa', CAST(0.00 AS Decimal(18, 2)), 1011, 4, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (14, N'/project/icono.jpg', N'Calidad y pruebas de software', N'Curso para realizar pruebas a tu sistema web, y optimizar ciertas partes, para obtener un sistema web 10 de 10', CAST(10.00 AS Decimal(18, 2)), 2014, 4, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (15, N'/FilesBD/maxresdefault.jpg', N'Anime', N'Anime chingon', CAST(150.00 AS Decimal(18, 2)), 2014, 1, 1)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (16, N'/FilesBD/unnamed.jpg', N'Cargar su imagen', N'curso sin imagen, inroducir sin imagen xde, y cargar con imagen, bueno guardar xde', CAST(10.00 AS Decimal(18, 2)), 2014, 2, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (17, N'/FilesBD/unnamed.jpg', N'Hola mundo', N'mundo hola', CAST(0.00 AS Decimal(18, 2)), 1011, 1, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (18, N'/project/icono.jpg', N'Ahorros del Año', N'Arreglar y cortar las flores', CAST(0.00 AS Decimal(18, 2)), 2014, 4, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (19, N'/project/icono.jpg', N'Ahorros del Año', N'Lavar los platos ', CAST(0.00 AS Decimal(18, 2)), 2011, 1, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (20, N'/project/icono.jpg', N'Ahorros del Año', N'Lavar los platos ', CAST(150.00 AS Decimal(18, 2)), 2011, 1, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (21, N'/project/icono.jpg', N'Hojas', N'Lavar los platos ', CAST(0.00 AS Decimal(18, 2)), 2011, 4, 0)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado]) VALUES (22, N'/project/icono.jpg', N'Hojas', N'Lavar los platos ', CAST(0.00 AS Decimal(18, 2)), 2011, 4, 0)
SET IDENTITY_INSERT [dbo].[Curso] OFF
GO
SET IDENTITY_INSERT [dbo].[CursoUsuario] ON 

INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (7, 1009, 7, 0, 0)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (8, 2014, 21, 0, 1)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (9, 2014, 1, 0, 0)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (10, 2014, 17, 0, 1)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (11, 2014, 22, 1, 1)
SET IDENTITY_INSERT [dbo].[CursoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Favoritos] ON 

INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (2, 1009, 7)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (19, 1009, 1)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (21, 2014, 1)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (25, 2014, 14)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (26, 1009, 19)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (27, 1009, 19)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (28, 1009, 5)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (31, 1009, 30)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (32, 1009, 16)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (34, 1009, 14)
SET IDENTITY_INSERT [dbo].[Favoritos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (2, N'Docente')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (3, N'Estudiante')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (1009, N'/FilesBD/photo_2021-06-27_21-25-15.jpg', N'Jhan Edwin', N'Toledo Ignacio', N'932182696', N'juliaRAF2020@gmail.com', N'JuliaS20TI', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 1, NULL, NULL, NULL, NULL, NULL, N'Ingeniero en sistemas', N'Desarrollador de software')
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (1011, N'/FilesBD/photo_2021-06-21_19-46-02.jpg', N'Miguel Anthony', N'Vásquez Cabanillas', N'123456', N'miguelanthony@gmail.com', N'MiguelC', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (2011, N'/FilesBD/maxresdefault.jpg', N'Jhan', N'Ignacio', N'999999999', N'jhan@gmail.com', N'Jhan123', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (2013, N'/project/userperfil.png', N'Edwin', N'Toledo', NULL, N'edwin@gmail.com', N'Edwin', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (2014, N'/FilesBD/unnamed.jpg', N'Edwin Jhordan', N'Vásquez Vilches', NULL, N'luckyxx12@gmail.com', N'EdwinVV', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
