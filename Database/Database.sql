USE [master]
GO
/****** Object:  Database [SynthenticFinancialManager]    Script Date: 11/7/2017 4:44:13 AM ******/
CREATE DATABASE [SynthenticFinancialManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'kaggle', FILENAME = N'd:\DATA\kaggle.mdf' , SIZE = 662528KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'kaggle_log', FILENAME = N'd:\DATA\kaggle_log.ldf' , SIZE = 7616KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SynthenticFinancialManager] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SynthenticFinancialManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SynthenticFinancialManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SynthenticFinancialManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SynthenticFinancialManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SynthenticFinancialManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SynthenticFinancialManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SynthenticFinancialManager] SET  MULTI_USER 
GO
ALTER DATABASE [SynthenticFinancialManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SynthenticFinancialManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SynthenticFinancialManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SynthenticFinancialManager] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SynthenticFinancialManager] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SynthenticFinancialManager]
GO
/****** Object:  Table [dbo].[BankTX]    Script Date: 11/7/2017 4:44:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankTX](
	[TxId] [int] IDENTITY(1,1) NOT NULL,
	[step] [int] NOT NULL,
	[type] [nvarchar](max) NULL,
	[amount] [real] NOT NULL,
	[nameOrig] [nvarchar](max) NULL,
	[oldbalanceOrg] [real] NOT NULL,
	[newbalanceOrig] [real] NOT NULL,
	[nameDest] [nvarchar](max) NULL,
	[oldbalanceDest] [real] NOT NULL,
	[newbalanceDest] [real] NOT NULL,
	[isFraud] [bit] NOT NULL,
	[isFlaggedFraud] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.BankTX] PRIMARY KEY CLUSTERED 
(
	[TxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 11/7/2017 4:44:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 11/7/2017 4:44:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL DEFAULT ((0)),
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL DEFAULT ((0)),
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 11/7/2017 4:44:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 11/7/2017 4:44:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 11/7/2017 4:44:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO
USE [master]
GO
ALTER DATABASE [SynthenticFinancialManager] SET  READ_WRITE 
GO
