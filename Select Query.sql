

TRUNCATE TABLE [ModelPortfolioLine]

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[extRef]
      ,[secRef]
      ,[secName]
      ,[percComp]
      ,[rate]
      ,[refPos]
      ,[LstDynmcRblRtDt]
  FROM [RaboBank].[dbo].[ModelPortfolioLine]