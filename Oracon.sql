USE [master]
GO
/****** Object:  Database [Oracon]    Script Date: 03/08/2021 0:48:48 ******/
CREATE DATABASE [Oracon]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Oracon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Oracon.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Oracon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Oracon_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Oracon] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Oracon].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Oracon] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Oracon] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Oracon] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Oracon] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Oracon] SET ARITHABORT OFF 
GO
ALTER DATABASE [Oracon] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Oracon] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Oracon] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Oracon] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Oracon] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Oracon] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Oracon] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Oracon] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Oracon] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Oracon] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Oracon] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Oracon] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Oracon] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Oracon] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Oracon] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Oracon] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Oracon] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Oracon] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Oracon] SET  MULTI_USER 
GO
ALTER DATABASE [Oracon] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Oracon] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Oracon] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Oracon] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Oracon] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Oracon] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Oracon] SET QUERY_STORE = OFF
GO
USE [Oracon]
GO
/****** Object:  Table [dbo].[Aprendizaje]    Script Date: 03/08/2021 0:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aprendizaje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCurso] [int] NULL,
	[Descripcion] [nvarchar](200) NULL,
 CONSTRAINT [PK_Aprendizaje] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 03/08/2021 0:48:49 ******/
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
/****** Object:  Table [dbo].[Clases]    Script Date: 03/08/2021 0:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdModulo] [int] NULL,
	[Nombre] [nvarchar](200) NULL,
	[Video] [nvarchar](100) NULL,
 CONSTRAINT [PK_Clases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComentarioCurso]    Script Date: 03/08/2021 0:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComentarioCurso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCurso] [int] NULL,
	[IdUsuario] [int] NULL,
	[Comentario] [nvarchar](1000) NULL,
 CONSTRAINT [PK_ComentarioCurso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 03/08/2021 0:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [nvarchar](50) NULL,
	[Nombre] [nvarchar](50) NULL,
	[Detalle] [nvarchar](250) NULL,
	[Descripcion] [nvarchar](2500) NULL,
	[Precio] [decimal](18, 2) NULL,
	[IdDocente] [int] NULL,
	[IdCategoria] [int] NULL,
	[Estado] [bit] NULL,
	[Video] [nvarchar](100) NULL,
 CONSTRAINT [PK_Cursos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CursoUsuario]    Script Date: 03/08/2021 0:48:49 ******/
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
/****** Object:  Table [dbo].[Favoritos]    Script Date: 03/08/2021 0:48:49 ******/
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
/****** Object:  Table [dbo].[Modulo]    Script Date: 03/08/2021 0:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modulo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCurso] [int] NULL,
	[Nombre] [nvarchar](200) NULL,
 CONSTRAINT [PK_Seccion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requisitos]    Script Date: 03/08/2021 0:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requisitos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCurso] [int] NULL,
	[Requisito] [nvarchar](200) NULL,
 CONSTRAINT [PK_Requisito] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/08/2021 0:48:49 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 03/08/2021 0:48:49 ******/
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
	[Biografia] [nvarchar](500) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Aprendizaje] ON 

INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (1, 16, N'Keep the trolling to a minimum. Do not mislead other people or spread false information about the ga')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (2, 14, N'Keep the trolling to a minimum. Do not mislead other people or spread false information about the ga')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (3, 18, N'Keep the trolling to a minimum. Do not mislead other people or spread false information about the ga')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (4, 16, N'Aprenderás cómo concluir un correo de negocios en inglés de forma efectiva y los pasos para hacer un proofreading antes de enviarlo.')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (1004, 1017, N'Aprenderás cómo concluir un correo de negocios en inglés de forma efectiva y los pasos para hacer un proofreading antes de enviarlo.')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (1005, 1017, N'Revisarás saludos formales en inglés y fórmulas ideales para comenzar un email de negocios en inglés con una introducción correcta.')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (1006, 1017, N'Descubrirás cómo escribir un email de trabajo en inglés para solicitar o brindar más información, hacer un reporte o para postular a una vacante.')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (1007, 1017, N'Aprenderás cuál es la estructura de un correo electrónico de negocios en inglés y qué considerar al escribir un correo formal o informal.')
INSERT [dbo].[Aprendizaje] ([Id], [IdCurso], [Descripcion]) VALUES (1008, 14, N'Utilizar Python para manipular todo tipo de información')
SET IDENTITY_INSERT [dbo].[Aprendizaje] OFF
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (1, N'Contabilidad')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (2, N'Administracion')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (3, N'Marketing')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (4, N'Ofimatica')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Clases] ON 

INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (1, 1, N'Before starting', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (2, 2, N'Structure + Choose an appropriate language', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (3, 2, N'Punctuation', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (4, 2, N'Grammar digital tools (Grammarly)', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (5, 2, N'Proyecto parte 1: Introduce yourself in an email format', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (6, 3, N'Effective subject lines', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (7, 3, N'Courtesy formulas and greetings', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (8, 3, N'Clarity is key', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (9, 3, N'Common expressions and business terms', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (10, 3, N'Let’s review some examples', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
INSERT [dbo].[Clases] ([Id], [IdModulo], [Nombre], [Video]) VALUES (11, 13, N'Empecemos con un video para motivarte', N'https://www.youtube.com/watch?v=qc_2kX3qbxM')
SET IDENTITY_INSERT [dbo].[Clases] OFF
GO
SET IDENTITY_INSERT [dbo].[ComentarioCurso] ON 

INSERT [dbo].[ComentarioCurso] ([Id], [IdCurso], [IdUsuario], [Comentario]) VALUES (1, 1017, 2014, N'en definitiva no es un curso para principiantes, el instructor agrega funciones o comandos sin explicarlas asumiendo que todos los que obtuvieron el curso tienen relacion con la programación, si quieres aprender desde 0 este curso no es para ti.')
INSERT [dbo].[ComentarioCurso] ([Id], [IdCurso], [IdUsuario], [Comentario]) VALUES (2, 1017, 2014, N'es muy lento y solo enseña jupyter, la base no es python')
INSERT [dbo].[ComentarioCurso] ([Id], [IdCurso], [IdUsuario], [Comentario]) VALUES (3, 1017, 2014, N'Hola mundo')
INSERT [dbo].[ComentarioCurso] ([Id], [IdCurso], [IdUsuario], [Comentario]) VALUES (4, 18, 2014, N'en definitiva no es un curso para principiantes, el instructor agrega funciones o comandos sin explicarlas asumiendo que todos los que obtuvieron el curso tienen relacion con la programación, si quieres aprender desde 0 este curso no es para ti.')
INSERT [dbo].[ComentarioCurso] ([Id], [IdCurso], [IdUsuario], [Comentario]) VALUES (5, 22, 2014, N'Muy malo')
SET IDENTITY_INSERT [dbo].[ComentarioCurso] OFF
GO
SET IDENTITY_INSERT [dbo].[Curso] ON 

INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (1, N'/FilesBD/unnamed.jpg', N'C++', N'Introduccion a la programacion con lenguaje C++', NULL, CAST(10.00 AS Decimal(18, 2)), 2011, 1, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (2, N'/project/icono.jpg', N'Boletas', N'Diseño, calculos de las boletas y facturas de ventas', NULL, CAST(1455.00 AS Decimal(18, 2)), 2011, 1, 1, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (3, N'/project/icono.jpg', N'Diseño de paginas', N'Diseño de paginas, online, marketing', NULL, CAST(0.00 AS Decimal(18, 2)), 2011, 3, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (4, N'/project/icono.jpg', N'Empresas Norte', N'Estudia y aprende, acerca de las empresas, como se administra una, y los registros que conlleva.', NULL, CAST(1455.00 AS Decimal(18, 2)), 2011, 2, 1, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (7, N'/project/icono.jpg', N'Perfiles Plantillas', N'Perfiles y plantillas', NULL, CAST(100.00 AS Decimal(18, 2)), 1011, 4, 1, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (12, N'/FilesBD/maxresdefault.jpg', N'Hojas', N'Lavar el auto de papa', NULL, CAST(0.00 AS Decimal(18, 2)), 1011, 4, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (14, N'/project/icono.jpg', N'Calidad y pruebas de software', N'Curso para realizar pruebas a tu sistema web, y optimizar ciertas partes, para obtener un sistema web 10 de 10', N'Incluye una unidad completa sobre inerfaces gráficas con Tkinter, otra sobre manejo de bases de datos SQLite y mucho más contenido.

Además en muchas lecciones se enseña con ejemplos y ejercicios reales de mi propia experiencia como programador, en lugar de simples ejemplos teóricos.

Es un curso innovador que utiliza herramientas avanzadas para apoyar el aprendizaje, como Jupyter Notebook, gracias al que serás capaz de crear tus propios apuntes a la vez que aprendes cada línea de código, y el editor Spyder para ejecutar tus programas con una simple combinación de teclas.

No esperes más y descubre por qué Python es el lenguaje de moda, mejora tus conocimientos y da un salto adelante en tu carrera profesional.', CAST(10.00 AS Decimal(18, 2)), 2014, 4, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (15, N'/FilesBD/maxresdefault.jpg', N'Anime', N'Anime chingon', NULL, CAST(150.00 AS Decimal(18, 2)), 2014, 1, 1, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (16, N'/FilesBD/unnamed.jpg', N'Cargar su imagen', N'curso sin imagen, inroducir sin imagen xde, y cargar con imagen, bueno guardar xde', NULL, CAST(10.00 AS Decimal(18, 2)), 2014, 2, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (17, N'/FilesBD/unnamed.jpg', N'Hola mundo', N'mundo hola', NULL, CAST(0.00 AS Decimal(18, 2)), 1011, 1, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (18, N'/project/icono.jpg', N'Ahorros del Año', N'Arreglar y cortar las flores', NULL, CAST(0.00 AS Decimal(18, 2)), 2014, 4, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (19, N'/project/icono.jpg', N'Ahorros del Año', N'Lavar los platos ', NULL, CAST(0.00 AS Decimal(18, 2)), 2011, 1, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (20, N'/project/icono.jpg', N'Ahorros del Año', N'Lavar los platos ', NULL, CAST(150.00 AS Decimal(18, 2)), 2011, 1, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (21, N'/project/icono.jpg', N'Hojas', N'Lavar los platos ', NULL, CAST(0.00 AS Decimal(18, 2)), 2011, 4, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (22, N'/project/icono.jpg', N'Hojas', N'Lavar los platos ', NULL, CAST(0.00 AS Decimal(18, 2)), 2011, 4, 0, NULL)
INSERT [dbo].[Curso] ([Id], [Imagen], [Nombre], [Detalle], [Descripcion], [Precio], [IdDocente], [IdCategoria], [Estado], [Video]) VALUES (1017, N'/FilesBD/26e473a2d66e26c12a7ab9e11bebd8a8.jpg', N'English for writing business emails', N'Escribe correos de negocios en inglés efectivos. Aprende la estructura y formatos más adecuados para cada nivel de formalidad que se presente.', N'Aprender a escribir correos en inglés efectivos, legibles y que cierren el trato es una habilidad necesaria para los negocios de hoy y la comunicación intercultural.

Por ello, en el curso de inglés intermedio English for writing business emails, revisarás técnicas y ejercicios de writing ideales para redactar un email de negocios en inglés que despegue tus proyectos laborales.

Masterizar la estructura del business writing, además de darle mayor fluidez a tu redacción de inglés para negocios, potenciará tu manejo de proyectos con clientes y colaboradores.

Junto a Veronica Ramirez, profesora de este curso de redacción en inglés intermedio online, aprenderás a escribir un email de negocios en inglés según el formato, situación y nivel de formalidad requerido. En estas clases de inglés online, entenderás cómo comenzar un correo electrónico en inglés, cuáles son los saludos y la etiqueta más adecuados, y cómo solicitar o brindar mayor información dentro de tus emails en inglés.

Al finalizar estas lecciones de escritura en inglés, entenderás cómo redactar un reporte y cómo escribir un email en inglés para solicitar un puesto de trabajo.', CAST(150.00 AS Decimal(18, 2)), 2014, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Curso] OFF
GO
SET IDENTITY_INSERT [dbo].[CursoUsuario] ON 

INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (7, 1009, 7, 0, 1)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (8, 2014, 21, 0, 0)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (9, 2014, 1, 0, 1)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (10, 2014, 17, 0, 0)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (11, 2014, 22, 1, 1)
INSERT [dbo].[CursoUsuario] ([Id], [IdUser], [idCurso], [Estado], [Pagado]) VALUES (1002, 1009, 22, 0, 1)
SET IDENTITY_INSERT [dbo].[CursoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Favoritos] ON 

INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (19, 1009, 1)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (21, 2014, 1)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (27, 1009, 19)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (28, 1009, 5)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (31, 1009, 30)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (1023, 2014, 14)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (1024, 2014, 1017)
INSERT [dbo].[Favoritos] ([Id], [IdUser], [IdCurso]) VALUES (1026, 2014, 0)
SET IDENTITY_INSERT [dbo].[Favoritos] OFF
GO
SET IDENTITY_INSERT [dbo].[Modulo] ON 

INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (1, 1017, N'Before starting')
INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (2, 1017, N'The fundamentals of a professional email')
INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (3, 1017, N'How to start your email')
INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (4, 1017, N'Common email situations')
INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (5, 1017, N'Let’s make an effective closing')
INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (6, 1017, N'Conclusiones')
INSERT [dbo].[Modulo] ([Id], [IdCurso], [Nombre]) VALUES (13, 14, N'Bienvenida')
SET IDENTITY_INSERT [dbo].[Modulo] OFF
GO
SET IDENTITY_INSERT [dbo].[Requisitos] ON 

INSERT [dbo].[Requisitos] ([Id], [IdCurso], [Requisito]) VALUES (1, 1017, N'Una computadora con acceso a Internet.')
INSERT [dbo].[Requisitos] ([Id], [IdCurso], [Requisito]) VALUES (2, 1017, N'Inglés nivel intermedio o básico-intermedio.')
SET IDENTITY_INSERT [dbo].[Requisitos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (2, N'Docente')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (3, N'Estudiante')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (1009, N'/FilesBD/photo_2021-06-27_21-25-15.jpg', N'Jhan Edwin', N'Toledo Ignacio', N'932182696', N'juliaRAF2020@gmail.com', N'JuliaS20TI', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 1, NULL, NULL, NULL, NULL, NULL, N'Ingeniero en sistemas', N'Desarrollador de software')
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (1011, N'/FilesBD/photo_2021-06-21_19-46-02.jpg', N'Miguel Anthony', N'Vásquez Cabanillas', N'123456', N'miguelanthony@gmail.com', N'MiguelC', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 2, NULL, NULL, NULL, NULL, NULL, N'Físico alemán', N'En 1905, cuando era un joven físico desconocido, empleado en la Oficina de Patentes de Berna, publicó su teoría de la relatividad especial. En ella incorporó, en un marco teórico simple fundamentado en postulados físicos sencillos, conceptos y fenómenos estudiados antes por Henri Poincaré y Hendrik Lorentz. Como una consecuencia lógica de esta teoría, dedujo la ecuación de la física más conocida a nivel popular: la equivalencia masa-energía, E=mc².')
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (2011, N'/FilesBD/maxresdefault.jpg', N'Jhan', N'Ignacio', N'999999999', N'jhan@gmail.com', N'Jhan123', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 2, NULL, NULL, NULL, NULL, NULL, N'Físico, teólogo, inventor, alquimista y matemático inglés', N'Es autor de los Philosophiæ naturalis principia mathematica, más conocidos como los Principia, donde describe la ley de la gravitación universal y estableció las bases de la mecánica clásica mediante las leyes que llevan su nombre. Entre sus otros descubrimientos científicos destacan los trabajos sobre la naturaleza de la luz y la óptica (que se presentan principalmente en su obra Opticks), y en matemáticas, el desarrollo del cálculo infinitesimal.')
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (2013, N'/project/userperfil.png', N'Edwin', N'Toledo', NULL, N'edwin@gmail.com', N'Edwin', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Usuario] ([Id], [Imagen], [Nombres], [Apellidos], [Celular], [Correo], [Username], [Password], [Recovery], [IdRol], [Twitter], [Facebook], [LinkedIn], [YouTube], [Instagram], [Titulo], [Biografia]) VALUES (2014, N'/FilesBD/unnamed.jpg', N'Edwin Jhordan', N'Vásquez Vilches', NULL, N'luckyxx12@gmail.com', N'EdwinVV', N'F+gLTtJi4gxG8d9vbgprZ5MyV5nssE5YhCtiR54gWI3OPZ2BR0LMMHe8IeW0h4jEiyruv35Sloeqc96JfW1rNA==', NULL, 2, NULL, NULL, NULL, NULL, NULL, N'Inventor, ingeniero eléctrico y mecánico serbio', N'Se le conoce sobre todo por sus numerosas invenciones en el campo del electromagnetismo, desarrolladas a finales del siglo XIX y principios del siglo XX.

Las patentes de Tesla y su trabajo teórico ayudaron a forjar las bases de los sistemas modernos para el uso de la energía eléctrica por corriente alterna (CA), lo que incluye el sistema polifásico de distribución eléctrica y el motor de corriente alterna, que contribuyeron al surgimiento de la Segunda Revolución Industrial.')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
USE [master]
GO
ALTER DATABASE [Oracon] SET  READ_WRITE 
GO
