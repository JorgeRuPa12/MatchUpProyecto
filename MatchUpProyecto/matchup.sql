/****** Object:  Table [dbo].[Deporte]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deporte](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[MinJugadoresEquipo] [int] NULL,
	[MaxJugadoresEquipo] [int] NULL,
	[Imagen] [nvarchar](255) NULL,
	[TiempoPartido] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipo]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipo](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Color] [nvarchar](50) NULL,
	[Deporte] [int] NULL,
	[Emblema] [nvarchar](255) NULL,
	[IdAdmin] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquiposLiga]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquiposLiga](
	[IdEquipo] [int] NOT NULL,
	[IdLiga] [int] NOT NULL,
	[Puntos] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEquipo] ASC,
	[IdLiga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquiposTorneo]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquiposTorneo](
	[IdTorneo] [int] NOT NULL,
	[IdEquipo] [int] NOT NULL,
	[Estado] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTorneo] ASC,
	[IdEquipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jornada]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jornada](
	[Id] [int] NOT NULL,
	[IdLiga] [int] NULL,
	[NumJornada] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JornadaPartido]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JornadaPartido](
	[IdJornada] [int] NOT NULL,
	[IdPartido] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdJornada] ASC,
	[IdPartido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Liga]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Liga](
	[Id] [int] NOT NULL,
	[Ganador] [int] NULL,
	[Ubicacion] [nvarchar](50) NULL,
	[Deporte] [int] NULL,
	[UbiLatitud] [nvarchar](100) NULL,
	[UbiLongitud] [nvarchar](100) NULL,
	[UbiProvincia] [nvarchar](50) NULL,
	[Inscripcion] [decimal](10, 2) NULL,
	[Acceso] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pachanga]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pachanga](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Ganador] [int] NULL,
	[Deporte] [int] NULL,
	[UbiLatitud] [nvarchar](100) NULL,
	[UbiLongitud] [nvarchar](100) NULL,
	[UbiProvincia] [nvarchar](50) NULL,
	[Inscripcion] [decimal](10, 2) NULL,
	[Estado] [nvarchar](10) NULL,
	[Acceso] [nvarchar](50) NULL,
	[Fecha] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PachangaPartido]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PachangaPartido](
	[IdPachanga] [int] NOT NULL,
	[IdPartido] [int] NOT NULL,
	[Estado] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPachanga] ASC,
	[IdPartido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partido]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partido](
	[Id] [int] NOT NULL,
	[Fecha] [datetime2](7) NULL,
	[EquipoLocal] [int] NULL,
	[EquipoVisitante] [int] NULL,
	[Resultado] [nvarchar](50) NULL,
	[UbiLatitud] [nvarchar](100) NULL,
	[UbiLongitud] [nvarchar](100) NULL,
	[UbiProvincia] [nvarchar](50) NULL,
	[Tiempo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ronda]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ronda](
	[Id] [int] NOT NULL,
	[IdTorneo] [int] NULL,
	[Etapa] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RondaPartido]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RondaPartido](
	[IdRonda] [int] NOT NULL,
	[IdPartido] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRonda] ASC,
	[IdPartido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Torneo]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Torneo](
	[Id] [int] NOT NULL,
	[Ganador] [int] NULL,
	[Ubicacion] [nvarchar](50) NULL,
	[Deporte] [int] NULL,
	[UbiLatitud] [nvarchar](100) NULL,
	[UbiLongitud] [nvarchar](100) NULL,
	[UbiProvincia] [nvarchar](50) NULL,
	[Inscripcion] [decimal](10, 2) NULL,
	[Acceso] [nvarchar](50) NULL,
	[NumEquipos] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ubicacion]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ubicacion](
	[IdUbicacion] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUbicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Imagen] [nvarchar](255) NULL,
	[Salt] [nvarchar](50) NULL,
	[Pass] [varbinary](max) NULL,
	[Rol] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioEquipo]    Script Date: 14/03/2025 10:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioEquipo](
	[IdUsuario] [int] NOT NULL,
	[IdEquipo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdEquipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Deporte] ([Id], [Nombre], [MinJugadoresEquipo], [MaxJugadoresEquipo], [Imagen], [TiempoPartido]) VALUES (1, N'Futbol 11', 11, 18, N'futbol11.jpg', 90)
INSERT [dbo].[Deporte] ([Id], [Nombre], [MinJugadoresEquipo], [MaxJugadoresEquipo], [Imagen], [TiempoPartido]) VALUES (2, N'Futbol Sala', 5, 10, N'futbolsala.jpg', 40)
INSERT [dbo].[Deporte] ([Id], [Nombre], [MinJugadoresEquipo], [MaxJugadoresEquipo], [Imagen], [TiempoPartido]) VALUES (3, N'Futbol 7', 7, 14, N'futbol7.jpg', 50)
GO
INSERT [dbo].[Equipo] ([Id], [Nombre], [Color], [Deporte], [Emblema], [IdAdmin]) VALUES (1, N'Tajamar', N'Negro', 1, N'emblema3.jpg', 1)
INSERT [dbo].[Equipo] ([Id], [Nombre], [Color], [Deporte], [Emblema], [IdAdmin]) VALUES (2, N'Atleti', N'Rojo', 1, N'emblema2.jpg', 2)
INSERT [dbo].[Equipo] ([Id], [Nombre], [Color], [Deporte], [Emblema], [IdAdmin]) VALUES (3, N'Real vardrid', N'Blanco', 1, N'emblema5.jpg', 3)
GO
INSERT [dbo].[Pachanga] ([Id], [Nombre], [Ganador], [Deporte], [UbiLatitud], [UbiLongitud], [UbiProvincia], [Inscripcion], [Estado], [Acceso], [Fecha]) VALUES (1, N'El Partidazo de Youtubers', 0, 1, N'40.02718312549688', N'-3.6200380325317387', N'Madrid', CAST(0.00 AS Decimal(10, 2)), N'Pendiente', N'Publico', CAST(N'2025-03-15T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Pachanga] ([Id], [Nombre], [Ganador], [Deporte], [UbiLatitud], [UbiLongitud], [UbiProvincia], [Inscripcion], [Estado], [Acceso], [Fecha]) VALUES (2, N'Champions Octavos', 0, 1, N'40.43607689817634', N'-3.5995888710021977', N'Madrid', CAST(50.00 AS Decimal(10, 2)), N'Pendiente', N'Privado', CAST(N'2025-03-12T21:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[PachangaPartido] ([IdPachanga], [IdPartido], [Estado]) VALUES (1, 1, N'Pendiente')
INSERT [dbo].[PachangaPartido] ([IdPachanga], [IdPartido], [Estado]) VALUES (2, 2, N'Pendiente')
GO
INSERT [dbo].[Partido] ([Id], [Fecha], [EquipoLocal], [EquipoVisitante], [Resultado], [UbiLatitud], [UbiLongitud], [UbiProvincia], [Tiempo]) VALUES (1, CAST(N'2025-03-15T11:00:00.0000000' AS DateTime2), 1, NULL, N'0', N'40.02718312549688', N'-3.6200380325317387', N'Madrid', 90)
INSERT [dbo].[Partido] ([Id], [Fecha], [EquipoLocal], [EquipoVisitante], [Resultado], [UbiLatitud], [UbiLongitud], [UbiProvincia], [Tiempo]) VALUES (2, CAST(N'2025-03-12T21:00:00.0000000' AS DateTime2), 2, 3, N'1 - 0', N'40.43607689817634', N'-3.5995888710021977', N'Madrid', 90)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Email], [Imagen], [Salt], [Pass], [Rol]) VALUES (1, N'Jorge', N'jorgerp12000@gmail.com', N'atardecer.jpg', N'üÝÃÚ(!äö§ºáûa·@ë@^Þ|]bí×&Ð`j6þºûú', 0xF2004B0C5DC1C25FBF5C1A6D95F73E329682753DF307EA55A4189914DAF4595ECBA62BA1E74EAD67971CBA1AAED18793B70A2703C5FFE501A29448F66C044C4C, N'2')
INSERT [dbo].[Usuario] ([Id], [Nombre], [Email], [Imagen], [Salt], [Pass], [Rol]) VALUES (2, N'Julian Alvarez', N'julian@gmail.com', N'perrosalchicha.jpg', N'\¤ä9°jµ×WÈµimÄÝå*ÇdöÀ;Ê4çkÇÜ|ÑË+â	æÔÉ', 0x7D00C37F52276C3387BCDE4C4CA2D20A7B585D30B6B7296387D17F5FD35782711A223A7ADEC199B852F1060C496AAC548B7D860B4096CE26909CBA0A6A6EEB0E, N'2')
INSERT [dbo].[Usuario] ([Id], [Nombre], [Email], [Imagen], [Salt], [Pass], [Rol]) VALUES (3, N'Vinicius', N'vini@gmail.com', N'simio.jpg', N'ÛQ[*d9ó¡[ï¾ ¦[ËõÓ©<:ìD!òÈÆêu.; Î¹á%(/ÄKë¢Å', 0xC13E6510C0BC658726EE4FC5F3A092CD857EC59C6AA136DD35189C8894021CBE5E459139148028D263672E87C87311BDC89E5AE73DE5420336B2E0358CE582DE, N'2')
GO
INSERT [dbo].[UsuarioEquipo] ([IdUsuario], [IdEquipo]) VALUES (1, 1)
INSERT [dbo].[UsuarioEquipo] ([IdUsuario], [IdEquipo]) VALUES (2, 1)
INSERT [dbo].[UsuarioEquipo] ([IdUsuario], [IdEquipo]) VALUES (2, 2)
INSERT [dbo].[UsuarioEquipo] ([IdUsuario], [IdEquipo]) VALUES (3, 3)
GO
ALTER TABLE [dbo].[EquiposLiga] ADD  DEFAULT ((0)) FOR [Puntos]
GO
ALTER TABLE [dbo].[Equipo]  WITH CHECK ADD FOREIGN KEY([Deporte])
REFERENCES [dbo].[Deporte] ([Id])
GO
ALTER TABLE [dbo].[Equipo]  WITH CHECK ADD FOREIGN KEY([IdAdmin])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[EquiposLiga]  WITH CHECK ADD FOREIGN KEY([IdEquipo])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[EquiposLiga]  WITH CHECK ADD FOREIGN KEY([IdLiga])
REFERENCES [dbo].[Liga] ([Id])
GO
ALTER TABLE [dbo].[EquiposTorneo]  WITH CHECK ADD FOREIGN KEY([IdEquipo])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[EquiposTorneo]  WITH CHECK ADD FOREIGN KEY([IdTorneo])
REFERENCES [dbo].[Torneo] ([Id])
GO
ALTER TABLE [dbo].[Jornada]  WITH CHECK ADD FOREIGN KEY([IdLiga])
REFERENCES [dbo].[Liga] ([Id])
GO
ALTER TABLE [dbo].[JornadaPartido]  WITH CHECK ADD FOREIGN KEY([IdJornada])
REFERENCES [dbo].[Jornada] ([Id])
GO
ALTER TABLE [dbo].[JornadaPartido]  WITH CHECK ADD FOREIGN KEY([IdPartido])
REFERENCES [dbo].[Partido] ([Id])
GO
ALTER TABLE [dbo].[Liga]  WITH CHECK ADD FOREIGN KEY([Deporte])
REFERENCES [dbo].[Deporte] ([Id])
GO
ALTER TABLE [dbo].[Liga]  WITH CHECK ADD FOREIGN KEY([Ganador])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[Pachanga]  WITH CHECK ADD FOREIGN KEY([Deporte])
REFERENCES [dbo].[Deporte] ([Id])
GO
ALTER TABLE [dbo].[PachangaPartido]  WITH CHECK ADD FOREIGN KEY([IdPachanga])
REFERENCES [dbo].[Pachanga] ([Id])
GO
ALTER TABLE [dbo].[PachangaPartido]  WITH CHECK ADD FOREIGN KEY([IdPartido])
REFERENCES [dbo].[Partido] ([Id])
GO
ALTER TABLE [dbo].[Partido]  WITH CHECK ADD FOREIGN KEY([EquipoLocal])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[Partido]  WITH CHECK ADD FOREIGN KEY([EquipoVisitante])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[Ronda]  WITH CHECK ADD FOREIGN KEY([IdTorneo])
REFERENCES [dbo].[Torneo] ([Id])
GO
ALTER TABLE [dbo].[RondaPartido]  WITH CHECK ADD FOREIGN KEY([IdPartido])
REFERENCES [dbo].[Partido] ([Id])
GO
ALTER TABLE [dbo].[RondaPartido]  WITH CHECK ADD FOREIGN KEY([IdRonda])
REFERENCES [dbo].[Ronda] ([Id])
GO
ALTER TABLE [dbo].[Torneo]  WITH CHECK ADD FOREIGN KEY([Deporte])
REFERENCES [dbo].[Deporte] ([Id])
GO
ALTER TABLE [dbo].[Torneo]  WITH CHECK ADD FOREIGN KEY([Ganador])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[UsuarioEquipo]  WITH CHECK ADD FOREIGN KEY([IdEquipo])
REFERENCES [dbo].[Equipo] ([Id])
GO
ALTER TABLE [dbo].[UsuarioEquipo]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO

UPDATE Partido
SET Fecha = '2025-03-13 11:00:00.0000000',
	EquipoVisitante = 2,
	Resultado = 'Por Determinar'
WHERE Id = 1;
 
UPDATE Pachanga
SET Fecha = '2025-03-13 11:00:00.0000000'
WHERE Id = 1;