/****** Object:  Table [dbo].[ModelPortfolioLine]    Script Date: 01-10-2020 12:24:05 ******/
DROP TABLE IF EXISTS [dbo].[ModelPortfolioLine]
GO
/****** Object:  Table [dbo].[ModelPortfolioLine]    Script Date: 01-10-2020 12:24:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModelPortfolioLine](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[extRef] [int] NULL,
	[secRef] [varchar](12) NULL,
	[secName] [varchar](50) NULL,
	[percComp] [decimal](38, 7) NULL,
	[rate] [decimal](38, 7) NULL,
	[refPos] [decimal](38, 7) NULL,
	[LstDynmcRblRtDt] [date] NULL
) ON [PRIMARY]
GO
