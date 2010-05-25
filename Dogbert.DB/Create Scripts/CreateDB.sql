USE [Dogbert]
GO
/****** Object:  Table [dbo].[UseCaseXUseCase]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UseCaseXUseCase](
	[ParentUseCaseID] [int] NOT NULL,
	[ChildUseCaseID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UseCaseSteps]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UseCaseSteps](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[UseCaseID] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Optional] [bit] NOT NULL,
	[DateAdded] [smalldatetime] NULL,
	[LastModified] [smalldatetime] NULL,
 CONSTRAINT [PK_UseCaseSteps] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[udf_RelatedUseCaseList]    Script Date: 05/25/2010 09:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	Get list of related usecase IDs
-- =============================================
CREATE FUNCTION [dbo].[udf_RelatedUseCaseList]
(
	-- Add the parameters for the function here
	@UseCaseID int
)
RETURNS varchar(MAX)
AS
BEGIN
	declare @ucList varchar(MAX)
	select @ucList = COALESCE(@ucList + ', ', '') + CAST(Child.id AS varchar(50))
	FROM         UseCases AS Parent INNER JOIN
                 UseCaseXUseCase ON Parent.id = UseCaseXUseCase.ParentUseCaseID INNER JOIN
                 UseCases AS Child ON UseCaseXUseCase.ChildUseCaseID = Child.id
	WHERE Parent.id = @UseCaseID

	return @ucList
END
GO
/****** Object:  Table [dbo].[UseCaseXRequirements]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UseCaseXRequirements](
	[UseCaseID] [int] NOT NULL,
	[RequirementID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTypes]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectTypes](
	[id] [char](2) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ProjectTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TextTypes]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TextTypes](
	[id] [char](2) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Priority] [int] NULL,
	[hasImage] [bit] NOT NULL,
 CONSTRAINT [PK_TextTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StatusCodes]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatusCodes](
	[id] [char](2) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsComplete] [bit] NOT NULL,
 CONSTRAINT [PK_StatusCodes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequirementTypes]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequirementTypes](
	[id] [char](2) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_RequirementTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PriorityTypes]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PriorityTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PriorityTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FileTypes]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FileTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UseCaseXActor]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UseCaseXActor](
	[UseCaseID] [int] NOT NULL,
	[ActorID] [int] NOT NULL,
 CONSTRAINT [PK_UseCaseXActor] PRIMARY KEY CLUSTERED 
(
	[UseCaseID] ASC,
	[ActorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actors]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Actors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vUserUnit]    Script Date: 05/25/2010 09:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUserUnit]
AS
SELECT     Catbert3.dbo.UserUnit.UnitID, Catbert3.dbo.UserUnit.UserID, Catbert3.dbo.Unit.FIS_Code
FROM         Catbert3.dbo.UserUnit INNER JOIN
                      Catbert3.dbo.Unit ON Catbert3.dbo.UserUnit.UnitID = Catbert3.dbo.Unit.UnitID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserUnit (Catbert3.dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 95
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Unit (Catbert3.dbo)"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 125
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUserUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUserUnit'
GO
/****** Object:  View [dbo].[vUsers]    Script Date: 05/25/2010 09:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUsers]
AS
SELECT     UserID, FirstName, LastName, EmployeeID, StudentID, UserImage, SID, Inactive, UserKey
FROM         Catbert3.dbo.Users AS Users
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUsers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUsers'
GO
/****** Object:  View [dbo].[vUnit]    Script Date: 05/25/2010 09:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUnit]
AS
SELECT     UnitID, FullName, ShortName, PPS_Code, FIS_Code, SchoolCode
FROM         Catbert3.dbo.Unit
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Unit (Catbert3.dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUnit'
GO
/****** Object:  View [dbo].[vLogin]    Script Date: 05/25/2010 09:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vLogin]
AS
SELECT     LoginID, UserID
FROM         Catbert3.dbo.Users
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users (Catbert3.dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vLogin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vLogin'
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Priority] [int] NULL,
	[Deadline] [smalldatetime] NULL,
	[Contact] [varchar](100) NOT NULL,
	[ContactEmail] [varchar](50) NULL,
	[Unit] [varchar](50) NOT NULL,
	[Complexity] [smallint] NULL,
	[ProjectedStart] [smalldatetime] NULL,
	[ProjectedEnd] [smalldatetime] NULL,
	[BeginDate] [smalldatetime] NULL,
	[EndDate] [smalldatetime] NULL,
	[StatusCode] [char](2) NOT NULL,
	[ProjectManagerID] [int] NULL,
	[LeadProgrammerID] [int] NULL,
	[ProjectTypeID] [char](2) NOT NULL,
	[DateAdded] [smalldatetime] NULL,
	[LastModified] [smalldatetime] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Requirements]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requirements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[RequirementTypeID] [char](2) NOT NULL,
	[PriorityTypeID] [int] NOT NULL,
	[TechnicalDifficulty] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[DateAdded] [smalldatetime] NOT NULL,
	[LastModified] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Requirements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[udf_usecaseRequirements]    Script Date: 05/25/2010 09:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[udf_usecaseRequirements]
(
	-- Add the parameters for the function here
	@UseCaseID int
)
RETURNS varchar(MAX)
AS
BEGIN
	declare @reqList varchar(MAX)
	select @reqList = COALESCE(@reqList + ', ', '') + CAST(Requirements.id AS varchar(50))
	FROM         Requirements INNER JOIN
                      UseCaseXRequirements ON Requirements.id = UseCaseXRequirements.RequirementID INNER JOIN
                      UseCases ON UseCaseXRequirements.UseCaseID = UseCases.id
	WHERE UseCases.id = @UseCaseID

	return @reqList
END
GO
/****** Object:  Table [dbo].[UseCases]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UseCases](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[DateAdded] [smalldatetime] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Precondition] [varchar](max) NOT NULL,
	[Postcondition] [varchar](max) NOT NULL,
	[LastModified] [smalldatetime] NOT NULL,
	[RequirementsList]  AS ([dbo].[udf_usecaseRequirements]([id])),
	[RelatedUseCaseList]  AS ([dbo].[udf_RelatedUseCaseList]([id])),
 CONSTRAINT [PK_UseCases] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequirementCategories]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequirementCategories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_RequirementCategories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectTexts]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectTexts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](max) NOT NULL,
	[TextTypeID] [char](2) NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectText] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectFiles]    Script Date: 05/25/2010 09:09:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectFiles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[FileTypeId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[FileContentType] [varchar](200) NOT NULL,
	[FileContents] [varbinary](max) NULL,
	[TextTypeId] [char](2) NULL,
	[Caption] [varchar](200) NULL,
 CONSTRAINT [PK_ProjectFiles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Actors_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Actors] ADD  CONSTRAINT [DF_Actors_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_PriorityTypes_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[PriorityTypes] ADD  CONSTRAINT [DF_PriorityTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_ProjectTypes_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[ProjectTypes] ADD  CONSTRAINT [DF_ProjectTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_RequirementCategories_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[RequirementCategories] ADD  CONSTRAINT [DF_RequirementCategories_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_RequirementTypes_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[RequirementTypes] ADD  CONSTRAINT [DF_RequirementTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_StatusCodes_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[StatusCodes] ADD  CONSTRAINT [DF_StatusCodes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_StatusCodes_IsComplete]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[StatusCodes] ADD  CONSTRAINT [DF_StatusCodes_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
GO
/****** Object:  Default [DF_TextTypes_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[TextTypes] ADD  CONSTRAINT [DF_TextTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_UseCases_DateAdded]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCases] ADD  CONSTRAINT [DF_UseCases_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
/****** Object:  Default [DF_Workers_IsActive]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Workers] ADD  CONSTRAINT [DF_Workers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_ProjectFiles_FileTypes]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[ProjectFiles]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFiles_FileTypes] FOREIGN KEY([FileTypeId])
REFERENCES [dbo].[FileTypes] ([id])
GO
ALTER TABLE [dbo].[ProjectFiles] CHECK CONSTRAINT [FK_ProjectFiles_FileTypes]
GO
/****** Object:  ForeignKey [FK_ProjectFiles_Projects]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[ProjectFiles]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFiles_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[ProjectFiles] CHECK CONSTRAINT [FK_ProjectFiles_Projects]
GO
/****** Object:  ForeignKey [FK_Projects_ProjectTypes]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTypes] FOREIGN KEY([ProjectTypeID])
REFERENCES [dbo].[ProjectTypes] ([id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectTypes]
GO
/****** Object:  ForeignKey [FK_Projects_StatusCodes]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_StatusCodes] FOREIGN KEY([StatusCode])
REFERENCES [dbo].[StatusCodes] ([id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_StatusCodes]
GO
/****** Object:  ForeignKey [FK_ProjectTexts_Projects]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[ProjectTexts]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTexts_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[ProjectTexts] CHECK CONSTRAINT [FK_ProjectTexts_Projects]
GO
/****** Object:  ForeignKey [FK_ProjectTexts_TextTypes]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[ProjectTexts]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTexts_TextTypes] FOREIGN KEY([TextTypeID])
REFERENCES [dbo].[TextTypes] ([id])
GO
ALTER TABLE [dbo].[ProjectTexts] CHECK CONSTRAINT [FK_ProjectTexts_TextTypes]
GO
/****** Object:  ForeignKey [FK_RequirementCategories_Projects]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[RequirementCategories]  WITH CHECK ADD  CONSTRAINT [FK_RequirementCategories_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[RequirementCategories] CHECK CONSTRAINT [FK_RequirementCategories_Projects]
GO
/****** Object:  ForeignKey [FK_Requirements_PriorityTypes]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Requirements]  WITH CHECK ADD  CONSTRAINT [FK_Requirements_PriorityTypes] FOREIGN KEY([PriorityTypeID])
REFERENCES [dbo].[PriorityTypes] ([id])
GO
ALTER TABLE [dbo].[Requirements] CHECK CONSTRAINT [FK_Requirements_PriorityTypes]
GO
/****** Object:  ForeignKey [FK_Requirements_Projects]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Requirements]  WITH CHECK ADD  CONSTRAINT [FK_Requirements_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[Requirements] CHECK CONSTRAINT [FK_Requirements_Projects]
GO
/****** Object:  ForeignKey [FK_Requirements_RequirementCategories]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Requirements]  WITH CHECK ADD  CONSTRAINT [FK_Requirements_RequirementCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[RequirementCategories] ([id])
GO
ALTER TABLE [dbo].[Requirements] CHECK CONSTRAINT [FK_Requirements_RequirementCategories]
GO
/****** Object:  ForeignKey [FK_Requirements_RequirementTypes]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[Requirements]  WITH CHECK ADD  CONSTRAINT [FK_Requirements_RequirementTypes] FOREIGN KEY([RequirementTypeID])
REFERENCES [dbo].[RequirementTypes] ([id])
GO
ALTER TABLE [dbo].[Requirements] CHECK CONSTRAINT [FK_Requirements_RequirementTypes]
GO
/****** Object:  ForeignKey [FK_UseCases_Projects]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCases]  WITH CHECK ADD  CONSTRAINT [FK_UseCases_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[UseCases] CHECK CONSTRAINT [FK_UseCases_Projects]
GO
/****** Object:  ForeignKey [FK_UseCases_RequirementCategories]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCases]  WITH CHECK ADD  CONSTRAINT [FK_UseCases_RequirementCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[RequirementCategories] ([id])
GO
ALTER TABLE [dbo].[UseCases] CHECK CONSTRAINT [FK_UseCases_RequirementCategories]
GO
/****** Object:  ForeignKey [FK_UseCaseSteps_UseCases]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCaseSteps]  WITH CHECK ADD  CONSTRAINT [FK_UseCaseSteps_UseCases] FOREIGN KEY([UseCaseID])
REFERENCES [dbo].[UseCases] ([id])
GO
ALTER TABLE [dbo].[UseCaseSteps] CHECK CONSTRAINT [FK_UseCaseSteps_UseCases]
GO
/****** Object:  ForeignKey [FK_UseCaseXActor_Actors]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCaseXActor]  WITH CHECK ADD  CONSTRAINT [FK_UseCaseXActor_Actors] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actors] ([id])
GO
ALTER TABLE [dbo].[UseCaseXActor] CHECK CONSTRAINT [FK_UseCaseXActor_Actors]
GO
/****** Object:  ForeignKey [FK_UseCaseXActor_UseCases]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCaseXActor]  WITH CHECK ADD  CONSTRAINT [FK_UseCaseXActor_UseCases] FOREIGN KEY([UseCaseID])
REFERENCES [dbo].[UseCases] ([id])
GO
ALTER TABLE [dbo].[UseCaseXActor] CHECK CONSTRAINT [FK_UseCaseXActor_UseCases]
GO
/****** Object:  ForeignKey [FK_UseCaseXUseCase_UseCases]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCaseXUseCase]  WITH CHECK ADD  CONSTRAINT [FK_UseCaseXUseCase_UseCases] FOREIGN KEY([ParentUseCaseID])
REFERENCES [dbo].[UseCases] ([id])
GO
ALTER TABLE [dbo].[UseCaseXUseCase] CHECK CONSTRAINT [FK_UseCaseXUseCase_UseCases]
GO
/****** Object:  ForeignKey [FK_UseCaseXUseCase_UseCases1]    Script Date: 05/25/2010 09:09:43 ******/
ALTER TABLE [dbo].[UseCaseXUseCase]  WITH CHECK ADD  CONSTRAINT [FK_UseCaseXUseCase_UseCases1] FOREIGN KEY([ChildUseCaseID])
REFERENCES [dbo].[UseCases] ([id])
GO
ALTER TABLE [dbo].[UseCaseXUseCase] CHECK CONSTRAINT [FK_UseCaseXUseCase_UseCases1]
GO
