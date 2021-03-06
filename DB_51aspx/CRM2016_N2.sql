USE [Easy2016]
GO
/****** Object:  Table [dbo].[CRM_Module]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Module](
	[ID] [varchar](50) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ParentID] [varchar](50) NULL,
	[Url] [varchar](200) NULL,
	[Sort] [int] NULL,
	[CreateUserID] [varchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[IsLast] [bit] NULL,
	[Version] [timestamp] NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_CRM_MODULE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CRM_Module] ([ID], [Name], [ParentID], [Url], [Sort], [CreateUserID], [CreateTime], [IsLast], [Enable]) VALUES (N'Cus', N'客户管理', N'0', NULL, 0, N'admin', CAST(0x0000A5BE0122B2C0 AS DateTime), 0, 1)
INSERT [dbo].[CRM_Module] ([ID], [Name], [ParentID], [Url], [Sort], [CreateUserID], [CreateTime], [IsLast], [Enable]) VALUES (N'CusInfo', N'客户信息管理', N'Cus', NULL, 0, N'admin', CAST(0x0000A5BF00A16279 AS DateTime), 0, 1)
INSERT [dbo].[CRM_Module] ([ID], [Name], [ParentID], [Url], [Sort], [CreateUserID], [CreateTime], [IsLast], [Enable]) VALUES (N'Customer', N'客户通讯', N'CusInfo', N'Customer/CRM_Customer', 1, N'admin', CAST(0x0000A5BF00A207B0 AS DateTime), 1, 1)
/****** Object:  Table [dbo].[CRM_Dept]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_Dept](
	[DeptID] [int] IDENTITY(1,1) NOT NULL,
	[DeptName] [nvarchar](50) NULL,
	[ParentID] [int] NULL,
	[Des] [nvarchar](50) NULL,
	[IsEnable] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_CRM_DEPT] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CRM_Dept] ON
INSERT [dbo].[CRM_Dept] ([DeptID], [DeptName], [ParentID], [Des], [IsEnable], [IsDelete]) VALUES (1, N'集团总公司', 0, N'总公司', 1, 0)
INSERT [dbo].[CRM_Dept] ([DeptID], [DeptName], [ParentID], [Des], [IsEnable], [IsDelete]) VALUES (3, N'IT部', 1, N'12312312313', 1, 0)
INSERT [dbo].[CRM_Dept] ([DeptID], [DeptName], [ParentID], [Des], [IsEnable], [IsDelete]) VALUES (4, N'研发一部', 3, N'研发一部', 1, 0)
INSERT [dbo].[CRM_Dept] ([DeptID], [DeptName], [ParentID], [Des], [IsEnable], [IsDelete]) VALUES (8, N'234234234', 4, N'234234', 1, 1)
SET IDENTITY_INSERT [dbo].[CRM_Dept] OFF
/****** Object:  Table [dbo].[CRM_RoleUser]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_RoleUser](
	[UserId] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[DeptId] [int] NULL,
 CONSTRAINT [PK_CRM_RoleUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CRM_RoleUser] ([UserId], [RoleId], [DeptId]) VALUES (N'admin', 1, NULL)
/****** Object:  Table [dbo].[SysException]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SysException](
	[Id] [varchar](50) NOT NULL,
	[HelpLink] [varchar](500) NULL,
	[Message] [varchar](500) NULL,
	[Source] [varchar](500) NULL,
	[StackTrace] [text] NULL,
	[TargetSite] [varchar](500) NULL,
	[Data] [varchar](500) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_SysException] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CRM_ModuleOperate]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_ModuleOperate](
	[ID] [varchar](200) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[KeyCode] [varchar](200) NOT NULL,
	[ModuleId] [varchar](50) NOT NULL,
	[IsValid] [bit] NOT NULL,
	[Sort] [int] NOT NULL,
 CONSTRAINT [PK_CRM_MODULEOPERATE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CRM_ModuleOperate] ([ID], [Name], [KeyCode], [ModuleId], [IsValid], [Sort]) VALUES (N'CustomerCreate', N'创建', N'Create', N'Customer', 1, 2)
INSERT [dbo].[CRM_ModuleOperate] ([ID], [Name], [KeyCode], [ModuleId], [IsValid], [Sort]) VALUES (N'CustomerQuery', N'查询', N'Query', N'Customer', 1, 1)
INSERT [dbo].[CRM_ModuleOperate] ([ID], [Name], [KeyCode], [ModuleId], [IsValid], [Sort]) VALUES (N'CustomerView', N'查看', N'View', N'Customer', 1, 0)
/****** Object:  Table [dbo].[CRM_Role]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Des] [nvarchar](50) NULL,
	[DeptId] [int] NOT NULL,
	[Enable] [bit] NOT NULL,
	[ParentId] [int] NOT NULL,
	[CopyRoleID] [int] NULL,
	[Level] [int] NOT NULL,
 CONSTRAINT [PK_CRM_ROLE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CRM_Role] ON
INSERT [dbo].[CRM_Role] ([ID], [Name], [Des], [DeptId], [Enable], [ParentId], [CopyRoleID], [Level]) VALUES (1, N'管理员', N'系统管理员', 1, 1, 0, 0, 0)
INSERT [dbo].[CRM_Role] ([ID], [Name], [Des], [DeptId], [Enable], [ParentId], [CopyRoleID], [Level]) VALUES (4, N'IT总监', N'123123', 3, 1, 0, 0, 0)
INSERT [dbo].[CRM_Role] ([ID], [Name], [Des], [DeptId], [Enable], [ParentId], [CopyRoleID], [Level]) VALUES (5, N'IT经理', N'234234', 3, 1, 4, 0, 0)
INSERT [dbo].[CRM_Role] ([ID], [Name], [Des], [DeptId], [Enable], [ParentId], [CopyRoleID], [Level]) VALUES (7, N'研发经理', N'123123', 4, 1, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[CRM_Role] OFF
/****** Object:  StoredProcedure [dbo].[P_Dept]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_Dept]
AS
BEGIN	
SET NOCOUNT ON;
declare @temp table(tid int,tlevel int,tsort varchar(1000))
declare @mlevel int
set @mlevel = 0
insert @temp select deptid,@mlevel,right('0000'+str(deptid,len(deptid)),4)
from CRM_Dept
where parentid = 0

while @@rowcount>0
begin
set @mlevel = @mlevel +1
insert @temp
select a.deptid,@mlevel,b.tsort+right('0000'+str(a.deptid,len(a.deptid)),4)
from CRM_Dept a,@temp b
where a.parentid = b.tid and b.tlevel = @mlevel -1
end

select a.DeptId,a.DeptName,a.Des,a.IsEnable,a.ParentId,a.ISDelete,b.tlevel as Level
from CRM_Dept a join @temp b on a.deptid = b.tid
order by b.tsort


END
GO
/****** Object:  UserDefinedFunction [dbo].[F_GetFullDeptName]    Script Date: 03/11/2016 17:28:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[F_GetFullDeptName] 
(
	-- 当前部门的上级部门ID
	@Pid int,
	-- 当前部门的名称
	@Name varchar(1000)	
)
RETURNS varchar(1000)
AS
BEGIN

	declare @temp varchar(50)	
	declare @id int
	set @id = 0
	select @id=ParentID,@temp=DeptName from CRM_Dept where DeptID=@Pid
	if @id = 0 and @Pid <> 0
		set @Name = @temp +' / '+ @Name;
	else if @id<>0
		set @Name =  dbo.F_GetFullDeptName(@id,@temp)+' / '+ @Name
		
	RETURN @Name;

END
GO
/****** Object:  Table [dbo].[CRM_User]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_User](
	[ID] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[Sex] [int] NULL,
	[Tel] [nvarchar](50) NULL,
	[RoleID] [int] NULL,
	[UserState] [int] NULL,
	[UserType] [int] NULL,
	[CreateUserID] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_CRM_USER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CRM_User] ([ID], [UserName], [PassWord], [Sex], [Tel], [RoleID], [UserState], [UserType], [CreateUserID], [CreateTime]) VALUES (N'admin', N'张三', N'123456', 1, N'123123432', 1, 1, 1, N'admin', CAST(0x0000A5AD00000000 AS DateTime))
/****** Object:  StoredProcedure [dbo].[P_Dept_]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_Dept_] 
AS
BEGIN

 select DeptID,DeptName,Des,ParentID,IsEnable,IsDelete,
 dbo.f_getfulldeptname(ParentID,DeptName) as Dept
 from CRM_Dept
 
END
GO
/****** Object:  Table [dbo].[CRM_Right]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_Right](
	[ID] [varchar](50) NOT NULL,
	[ModulID] [varchar](50) NOT NULL,
	[RightFlag] [bit] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_CRM_RIGHT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CRM_Right] ([ID], [ModulID], [RightFlag], [RoleID]) VALUES (N'1Customer', N'Customer', 1, 1)
/****** Object:  View [dbo].[V_DeptUser]    Script Date: 03/11/2016 17:28:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_DeptUser]
AS
SELECT     t1.ID, t3.ID AS RoleID,t3.Name AS RoleName, t3.DeptId, t3.ParentId, t4.DeptName, t1.UserName
FROM         dbo.CRM_User AS t1 LEFT OUTER JOIN
                      dbo.CRM_RoleUser AS t2 ON t1.ID = t2.UserId LEFT OUTER JOIN
                      dbo.CRM_Role AS t3 ON t2.RoleId = t3.ID LEFT OUTER JOIN
                      dbo.CRM_Dept AS t4 ON t3.DeptId = t4.DeptID
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
         Begin Table = "t4"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t3"
            Begin Extent = 
               Top = 6
               Left = 218
               Bottom = 125
               Right = 364
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t2"
            Begin Extent = 
               Top = 6
               Left = 402
               Bottom = 110
               Right = 544
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t1"
            Begin Extent = 
               Top = 6
               Left = 582
               Bottom = 125
               Right = 737
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
      Begin ColumnWidths = 28
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_DeptUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'        Width = 1500
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_DeptUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_DeptUser'
GO
/****** Object:  Table [dbo].[CRM_RightOperate]    Script Date: 03/11/2016 17:27:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CRM_RightOperate](
	[ID] [varchar](50) NOT NULL,
	[IsValid] [bit] NOT NULL,
	[KeyCode] [varchar](50) NOT NULL,
	[RightID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CRM_RIGHTOPERATE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CRM_RightOperate] ([ID], [IsValid], [KeyCode], [RightID]) VALUES (N'1CustomerCreate', 1, N'Create', N'1Customer')
INSERT [dbo].[CRM_RightOperate] ([ID], [IsValid], [KeyCode], [RightID]) VALUES (N'1CustomerQuery', 1, N'Query', N'1Customer')
INSERT [dbo].[CRM_RightOperate] ([ID], [IsValid], [KeyCode], [RightID]) VALUES (N'1CustomerView', 1, N'View', N'1Customer')
/****** Object:  ForeignKey [FK_CRM_MODU_REFERENCE_CRM_MODU]    Script Date: 03/11/2016 17:27:59 ******/
ALTER TABLE [dbo].[CRM_ModuleOperate]  WITH CHECK ADD  CONSTRAINT [FK_CRM_MODU_REFERENCE_CRM_MODU] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[CRM_Module] ([ID])
GO
ALTER TABLE [dbo].[CRM_ModuleOperate] CHECK CONSTRAINT [FK_CRM_MODU_REFERENCE_CRM_MODU]
GO
/****** Object:  ForeignKey [FK_CRM_RIGH_REFERENCE_CRM_MODU]    Script Date: 03/11/2016 17:27:59 ******/
ALTER TABLE [dbo].[CRM_Right]  WITH CHECK ADD  CONSTRAINT [FK_CRM_RIGH_REFERENCE_CRM_MODU] FOREIGN KEY([ModulID])
REFERENCES [dbo].[CRM_Module] ([ID])
GO
ALTER TABLE [dbo].[CRM_Right] CHECK CONSTRAINT [FK_CRM_RIGH_REFERENCE_CRM_MODU]
GO
/****** Object:  ForeignKey [FK_CRM_RIGH_REFERENCE_CRM_ROLE]    Script Date: 03/11/2016 17:27:59 ******/
ALTER TABLE [dbo].[CRM_Right]  WITH CHECK ADD  CONSTRAINT [FK_CRM_RIGH_REFERENCE_CRM_ROLE] FOREIGN KEY([RoleID])
REFERENCES [dbo].[CRM_Role] ([ID])
GO
ALTER TABLE [dbo].[CRM_Right] CHECK CONSTRAINT [FK_CRM_RIGH_REFERENCE_CRM_ROLE]
GO
/****** Object:  ForeignKey [FK_CRM_RIGH_REFERENCE_CRM_RIGH]    Script Date: 03/11/2016 17:27:59 ******/
ALTER TABLE [dbo].[CRM_RightOperate]  WITH CHECK ADD  CONSTRAINT [FK_CRM_RIGH_REFERENCE_CRM_RIGH] FOREIGN KEY([RightID])
REFERENCES [dbo].[CRM_Right] ([ID])
GO
ALTER TABLE [dbo].[CRM_RightOperate] CHECK CONSTRAINT [FK_CRM_RIGH_REFERENCE_CRM_RIGH]
GO
/****** Object:  ForeignKey [FK_CRM_ROLE_REFERENCE_CRM_DEPT]    Script Date: 03/11/2016 17:27:59 ******/
ALTER TABLE [dbo].[CRM_Role]  WITH CHECK ADD  CONSTRAINT [FK_CRM_ROLE_REFERENCE_CRM_DEPT] FOREIGN KEY([DeptId])
REFERENCES [dbo].[CRM_Dept] ([DeptID])
GO
ALTER TABLE [dbo].[CRM_Role] CHECK CONSTRAINT [FK_CRM_ROLE_REFERENCE_CRM_DEPT]
GO
/****** Object:  ForeignKey [FK_CRM_USER_REFERENCE_CRM_ROLE]    Script Date: 03/11/2016 17:27:59 ******/
ALTER TABLE [dbo].[CRM_User]  WITH CHECK ADD  CONSTRAINT [FK_CRM_USER_REFERENCE_CRM_ROLE] FOREIGN KEY([RoleID])
REFERENCES [dbo].[CRM_Role] ([ID])
GO
ALTER TABLE [dbo].[CRM_User] CHECK CONSTRAINT [FK_CRM_USER_REFERENCE_CRM_ROLE]
GO
