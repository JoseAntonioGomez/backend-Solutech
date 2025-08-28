# backend-Solutech

Query de las tablas creadas en SQL Server


CREATE TABLE [dbo].[Usuario](
	[usuarioID] [int] IDENTITY(1,1) NOT NULL,
	[nombreUsuario] [varchar](50) NOT NULL,
	[passwordUsuario] [varchar](100) NULL,
	[ultimoAcceso] [datetime] NOT NULL,
	[estado] [bit] NOT NULL,
	[fechaCreacion] [datetime] NULL,
	[fechaModificacion] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[usuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[Tareas](
	[tareaID] [int] IDENTITY(1,1) NOT NULL,
	[usuarioID] [int] NOT NULL,
	[titulo] [nvarchar](100) NOT NULL,
	[descripcion] [nvarchar](255) NULL,
	[estado] [bit] NOT NULL,
	[fechaCreacion] [datetime] NOT NULL,
	[fechaVencimiento] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tareaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tareas]  WITH CHECK ADD  CONSTRAINT [FK_Tareas_Usuario] FOREIGN KEY([usuarioID])
REFERENCES [dbo].[Usuario] ([usuarioID])
GO

ALTER TABLE [dbo].[Tareas] CHECK CONSTRAINT [FK_Tareas_Usuario]
GO
