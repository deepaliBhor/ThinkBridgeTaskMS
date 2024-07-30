USE [master]
GO
/****** Object:  Database [TaskManagement]    Script Date: 7/23/2024 9:56:52 PM ******/
CREATE DATABASE [TaskManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TaskManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TaskManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TaskManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaskManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaskManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TaskManagement] SET  MULTI_USER 
GO
ALTER DATABASE [TaskManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaskManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaskManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TaskManagement] SET QUERY_STORE = OFF
GO
USE [TaskManagement]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/23/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 7/23/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[sFilePath] [nvarchar](500) NOT NULL,
	[nTaskId] [int] NOT NULL,
	[dtCreatedAt] [datetime] NOT NULL,
	[dtUpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/23/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[sName] [nvarchar](100) NOT NULL,
	[sEmail] [nvarchar](100) NOT NULL,
	[nManagerId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 7/23/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[sContent] [nvarchar](max) NOT NULL,
	[nTaskId] [int] NOT NULL,
	[dtCreatedAt] [datetime] NOT NULL,
	[dtUpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 7/23/2024 9:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[nId] [int] IDENTITY(1,1) NOT NULL,
	[sTitle] [nvarchar](200) NOT NULL,
	[sDescription] [nvarchar](max) NULL,
	[dtDueDate] [datetime] NOT NULL,
	[sStatus] [nvarchar](50) NOT NULL,
	[nEmployeeId] [int] NOT NULL,
	[dtCreatedAt] [datetime] NOT NULL,
	[dtUpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[nId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([nId], [sName], [sEmail], [nManagerId]) VALUES (1, N'John Doe', N'john.doe@company.com', 1)
GO
INSERT [dbo].[Employee] ([nId], [sName], [sEmail], [nManagerId]) VALUES (2, N'Jane Roe', N'jane.roe@company.com', 1)
GO
INSERT [dbo].[Employee] ([nId], [sName], [sEmail], [nManagerId]) VALUES (3, N'Mark Twain', N'mark.twain@company.com', 2)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Note] ON 
GO
INSERT [dbo].[Note] ([nId], [sContent], [nTaskId], [dtCreatedAt], [dtUpdatedAt]) VALUES (1, N'Discussed with team about the report structure', 1, CAST(N'2024-07-21T16:12:54.113' AS DateTime), NULL)
GO
INSERT [dbo].[Note] ([nId], [sContent], [nTaskId], [dtCreatedAt], [dtUpdatedAt]) VALUES (2, N'Added new slides for the financial overview', 2, CAST(N'2024-07-21T16:12:54.113' AS DateTime), NULL)
GO
INSERT [dbo].[Note] ([nId], [sContent], [nTaskId], [dtCreatedAt], [dtUpdatedAt]) VALUES (3, N'Identified the root cause of the bug in module X', 3, CAST(N'2024-07-21T16:12:54.113' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Note] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 
GO
INSERT [dbo].[Tasks] ([nId], [sTitle], [sDescription], [dtDueDate], [sStatus], [nEmployeeId], [dtCreatedAt], [dtUpdatedAt]) VALUES (1, N'Complete project report', N'Finish the final report for the project', CAST(N'2024-07-25T00:00:00.000' AS DateTime), N'0', 1, CAST(N'2024-07-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Tasks] ([nId], [sTitle], [sDescription], [dtDueDate], [sStatus], [nEmployeeId], [dtCreatedAt], [dtUpdatedAt]) VALUES (2, N'Prepare presentation', N'Create slides for the upcoming presentation', CAST(N'2024-07-30T00:00:00.000' AS DateTime), N'0', 2, CAST(N'2024-07-15T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Tasks] ([nId], [sTitle], [sDescription], [dtDueDate], [sStatus], [nEmployeeId], [dtCreatedAt], [dtUpdatedAt]) VALUES (3, N'Fix bugs in the system', N'Resolve the bugs found during testing', CAST(N'2024-08-01T00:00:00.000' AS DateTime), N'0', 3, CAST(N'2024-07-17T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Tasks] ([nId], [sTitle], [sDescription], [dtDueDate], [sStatus], [nEmployeeId], [dtCreatedAt], [dtUpdatedAt]) VALUES (4, N'Prepare presentation', N'Create slides for the upcoming presentation', CAST(N'2024-08-01T00:00:00.000' AS DateTime), N'0', 3, CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Employee__07DACB08F9197462]    Script Date: 7/23/2024 9:56:53 PM ******/
ALTER TABLE [dbo].[Employee] ADD UNIQUE NONCLUSTERED 
(
	[sEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Document] ADD  DEFAULT (getdate()) FOR [dtCreatedAt]
GO
ALTER TABLE [dbo].[Note] ADD  DEFAULT (getdate()) FOR [dtCreatedAt]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT (getdate()) FOR [dtCreatedAt]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD FOREIGN KEY([nTaskId])
REFERENCES [dbo].[Tasks] ([nId])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([nManagerId])
REFERENCES [dbo].[Employee] ([nId])
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD FOREIGN KEY([nTaskId])
REFERENCES [dbo].[Tasks] ([nId])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([nEmployeeId])
REFERENCES [dbo].[Employee] ([nId])
GO
USE [master]
GO
ALTER DATABASE [TaskManagement] SET  READ_WRITE 
GO
