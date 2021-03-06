USE [master]
GO
/****** Object:  Database [ERetailShop]    Script Date: 12/29/2015 12:41:48 AM ******/
CREATE DATABASE [ERetailShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ERetailShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ERetailShop.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ERetailShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ERetailShop_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ERetailShop] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ERetailShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ERetailShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ERetailShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ERetailShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ERetailShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ERetailShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [ERetailShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ERetailShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ERetailShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ERetailShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ERetailShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ERetailShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ERetailShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ERetailShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ERetailShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ERetailShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ERetailShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ERetailShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ERetailShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ERetailShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ERetailShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ERetailShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ERetailShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ERetailShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ERetailShop] SET  MULTI_USER 
GO
ALTER DATABASE [ERetailShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ERetailShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ERetailShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ERetailShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ERetailShop] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ERetailShop]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/29/2015 12:41:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[State] [varchar](100) NOT NULL,
	[City] [varchar](100) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/29/2015 12:41:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[Fk_CustomerId] [int] NOT NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

GO
INSERT [dbo].[Customer] ([Id], [Name], [State], [City]) VALUES (1, N'Shamajit Manna', N'West Bengal', N'Kolkata')
GO
INSERT [dbo].[Customer] ([Id], [Name], [State], [City]) VALUES (2, N'Prabhash', N'Andra Pradesh', N'Andhra Pradesh')
GO
INSERT [dbo].[Customer] ([Id], [Name], [State], [City]) VALUES (3, N'Atul ', N'Madhya Pradesh', N'Nasik')
GO
INSERT [dbo].[Customer] ([Id], [Name], [State], [City]) VALUES (4, N'Aamir Rastogi', N'Maharashtra', N'Mumbai')
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (1, CAST(N'2015-12-12' AS Date), 1)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (2, CAST(N'2015-12-26' AS Date), 1)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (3, CAST(N'2015-12-26' AS Date), 1)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (4, CAST(N'2015-11-26' AS Date), 1)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (5, CAST(N'2015-12-26' AS Date), 1)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (6, CAST(N'2015-12-26' AS Date), 3)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (7, CAST(N'2016-01-26' AS Date), 2)
GO
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [Fk_CustomerId]) VALUES (8, CAST(N'2015-12-26' AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
/****** Object:  StoredProcedure [dbo].[AddCustomer]    Script Date: 12/29/2015 12:41:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[AddCustomer]
@Name VARCHAR(200),
@City VARCHAR(100),
@State VARCHAR(100),
@CustomerId INT OUT

AS

INSERT INTO Customer(Name,City,[State]) VALUES (@Name,@City,@State)

SET @CustomerId = @@IDENTITY;



GO
/****** Object:  StoredProcedure [dbo].[AddNewCustomer]    Script Date: 12/29/2015 12:41:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[AddNewCustomer]
@Name VARCHAR(200),
@City VARCHAR(100),
@State VARCHAR(100)

AS

INSERT INTO Customer(Name,City,[State]) VALUES (@Name,@City,@State)

RETURN @@identity;



GO
/****** Object:  StoredProcedure [dbo].[GetAllCustomer]    Script Date: 12/29/2015 12:41:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllCustomer]

AS

SELECT Id,Name,[State], City FROM Customer;

GO
/****** Object:  StoredProcedure [dbo].[GetAllCustomerAndOrder]    Script Date: 12/29/2015 12:41:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllCustomerAndOrder]
AS

SELECT Id, Name, City , [State] FROM Customer;

SELECT OrderId, OrderDate, FK_CustomerId AS CustomerId FROM Orders;
GO
USE [master]
GO
ALTER DATABASE [ERetailShop] SET  READ_WRITE 
GO
