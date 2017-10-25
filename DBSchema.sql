/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2012 (11.0.6020)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [NL_Example]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 25/10/2017 12:38:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colour]    Script Date: 25/10/2017 12:38:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colour](
	[ColourId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED 
(
	[ColourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stuff]    Script Date: 25/10/2017 12:38:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stuff](
	[StuffId] [int] IDENTITY(1,1) NOT NULL,
	[One] [nvarchar](128) NULL,
	[Two] [nvarchar](128) NULL,
	[Three] [nvarchar](128) NULL,
	[ColourId] [int] NULL,
 CONSTRAINT [PK_Stuff] PRIMARY KEY CLUSTERED 
(
	[StuffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StuffCategory]    Script Date: 25/10/2017 12:38:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StuffCategory](
	[StuffId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_StuffCategory] PRIMARY KEY CLUSTERED 
(
	[StuffId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 25/10/2017 12:38:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[TokenId] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [nvarchar](128) NOT NULL,
	[System] [nvarchar](128) NOT NULL,
	[LastAccessed] [datetime] NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Token] ADD  CONSTRAINT [DF_Token_Token]  DEFAULT (newid()) FOR [Guid]
GO
ALTER TABLE [dbo].[Token] ADD  CONSTRAINT [DF_Token_LastAccessed]  DEFAULT (getdate()) FOR [LastAccessed]
GO
ALTER TABLE [dbo].[Stuff]  WITH CHECK ADD  CONSTRAINT [FK_Stuff_Child] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colour] ([ColourId])
GO
ALTER TABLE [dbo].[Stuff] CHECK CONSTRAINT [FK_Stuff_Child]
GO
ALTER TABLE [dbo].[StuffCategory]  WITH CHECK ADD  CONSTRAINT [FK_StuffCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[StuffCategory] CHECK CONSTRAINT [FK_StuffCategory_Category]
GO
ALTER TABLE [dbo].[StuffCategory]  WITH CHECK ADD  CONSTRAINT [FK_StuffCategory_Stuff] FOREIGN KEY([StuffId])
REFERENCES [dbo].[Stuff] ([StuffId])
GO
ALTER TABLE [dbo].[StuffCategory] CHECK CONSTRAINT [FK_StuffCategory_Stuff]
GO
