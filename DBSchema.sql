/****** Object:  Table [dbo].[Stuff]    Script Date: 19/10/2017 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stuff](
	[StuffId] [int] IDENTITY(1,1) NOT NULL,
	[One] [nvarchar](128) NULL,
	[Two] [nvarchar](128) NULL,
	[Three] [nvarchar](128) NULL,
 CONSTRAINT [PK_Stuff] PRIMARY KEY CLUSTERED 
(
	[StuffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 19/10/2017 15:27:07 ******/
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
