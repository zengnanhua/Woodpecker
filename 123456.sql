USE [master]
GO
/****** Object:  Database [Web]    Script Date: 08/17/2016 17:58:32 ******/
CREATE DATABASE [Web] ON  PRIMARY 
( NAME = N'Web', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Web.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Web_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Web_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Web] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Web].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Web] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Web] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Web] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Web] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Web] SET ARITHABORT OFF
GO
ALTER DATABASE [Web] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Web] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Web] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Web] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Web] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Web] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Web] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Web] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Web] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Web] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Web] SET  DISABLE_BROKER
GO
ALTER DATABASE [Web] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Web] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Web] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Web] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Web] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Web] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Web] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Web] SET  READ_WRITE
GO
ALTER DATABASE [Web] SET RECOVERY FULL
GO
ALTER DATABASE [Web] SET  MULTI_USER
GO
ALTER DATABASE [Web] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Web] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'Web', N'ON'
GO
USE [Web]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 08/17/2016 17:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](64) NULL,
	[FatherID] [int] NULL,
	[Area] [varchar](64) NULL,
	[ControllerName] [varchar](64) NULL,
	[ActionName] [varchar](64) NULL,
	[OrderNo] [int] NULL,
	[IsDelete] [bit] NULL,
	[Type] [int] NULL,
	[Method] [int] NULL,
 CONSTRAINT [PK_MENU] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MenuId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'FatherID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Area'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'ControllerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'action方法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0位菜单，1为功能' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu'
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 08/17/2016 17:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserInfoId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](128) NULL,
	[Pwd] [varchar](128) NULL,
	[HeadImgPath] [varchar](128) NULL,
 CONSTRAINT [PK_USERINFO] PRIMARY KEY CLUSTERED 
(
	[UserInfoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UserInfoId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Pwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'HeadImgPath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo'
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON
INSERT [dbo].[UserInfo] ([UserInfoId], [UserName], [Pwd], [HeadImgPath]) VALUES (1, N'admin', N'E10ADC3949BA59ABBE56E057F20F883E', N'http://img4.imgtn.bdimg.com/it/u=1896910380,334902664&fm=21&gp=0.jpg')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 08/17/2016 17:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](128) NULL,
	[RoleDiscription] [varchar](256) NULL,
	[IsLoginBack] [bit] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RoleId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'RoleDiscription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以登录后台' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'IsLoginBack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role'
GO
/****** Object:  Table [dbo].[R_UserInfo_Role]    Script Date: 08/17/2016 17:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[R_UserInfo_Role](
	[R_UserInfo_RoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](128) NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_R_USERINFO_ROLE] PRIMARY KEY CLUSTERED 
(
	[R_UserInfo_RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'R_UserInfo_RoleId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_UserInfo_Role', @level2type=N'COLUMN',@level2name=N'R_UserInfo_RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UserName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_UserInfo_Role', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RoleId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_UserInfo_Role', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'R_UserInfo_Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_UserInfo_Role'
GO
/****** Object:  Table [dbo].[R_Role_Menu]    Script Date: 08/17/2016 17:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_Role_Menu](
	[R_Role_MenuId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[MenuId] [int] NULL,
 CONSTRAINT [PK_R_ROLE_MENU] PRIMARY KEY CLUSTERED 
(
	[R_Role_MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'R_Role_MenuId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_Role_Menu', @level2type=N'COLUMN',@level2name=N'R_Role_MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RoleId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_Role_Menu', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MenuId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_Role_Menu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'R_Role_Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'R_Role_Menu'
GO
/****** Object:  Table [dbo].[OtherMenu]    Script Date: 08/17/2016 17:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OtherMenu](
	[OtherMenuId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](128) NULL,
	[MenuId] [int] NULL,
 CONSTRAINT [PK_OTHERMENU] PRIMARY KEY CLUSTERED 
(
	[OtherMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OtherMenuId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OtherMenu', @level2type=N'COLUMN',@level2name=N'OtherMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UserName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OtherMenu', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MenuId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OtherMenu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OtherMenu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OtherMenu'
GO
/****** Object:  ForeignKey [FK_R_ROLE_M_REFERENCE_MENU]    Script Date: 08/17/2016 17:58:34 ******/
ALTER TABLE [dbo].[R_Role_Menu]  WITH CHECK ADD  CONSTRAINT [FK_R_ROLE_M_REFERENCE_MENU] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[R_Role_Menu] CHECK CONSTRAINT [FK_R_ROLE_M_REFERENCE_MENU]
GO
/****** Object:  ForeignKey [FK_R_ROLE_M_REFERENCE_ROLE]    Script Date: 08/17/2016 17:58:34 ******/
ALTER TABLE [dbo].[R_Role_Menu]  WITH CHECK ADD  CONSTRAINT [FK_R_ROLE_M_REFERENCE_ROLE] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[R_Role_Menu] CHECK CONSTRAINT [FK_R_ROLE_M_REFERENCE_ROLE]
GO
/****** Object:  ForeignKey [FK_OTHERMEN_REFERENCE_MENU]    Script Date: 08/17/2016 17:58:34 ******/
ALTER TABLE [dbo].[OtherMenu]  WITH CHECK ADD  CONSTRAINT [FK_OTHERMEN_REFERENCE_MENU] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[OtherMenu] CHECK CONSTRAINT [FK_OTHERMEN_REFERENCE_MENU]
GO
