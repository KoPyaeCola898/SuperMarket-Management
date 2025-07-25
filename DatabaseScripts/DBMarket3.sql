USE [master]
GO
/****** Object:  Database [DBMarket]    Script Date: 7/21/2025 12:19:20 PM ******/
CREATE DATABASE [DBMarket]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBMarket', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DBMarket.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBMarket_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DBMarket_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBMarket] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBMarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBMarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBMarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBMarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBMarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBMarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBMarket] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBMarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBMarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBMarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBMarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBMarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBMarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBMarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBMarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBMarket] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBMarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBMarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBMarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBMarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBMarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBMarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBMarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBMarket] SET RECOVERY FULL 
GO
ALTER DATABASE [DBMarket] SET  MULTI_USER 
GO
ALTER DATABASE [DBMarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBMarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBMarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBMarket] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBMarket] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBMarket] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBMarket', N'ON'
GO
ALTER DATABASE [DBMarket] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBMarket] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBMarket]
GO
/****** Object:  Table [dbo].[tbProduct]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbProduct](
	[pcode] [varchar](50) NOT NULL,
	[barcode] [varchar](50) NULL,
	[item] [varchar](max) NOT NULL,
	[cid] [int] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[qty] [int] NULL,
	[reorder] [int] NULL,
 CONSTRAINT [PK_tbProduct] PRIMARY KEY CLUSTERED 
(
	[pcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbStockIn]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbStockIn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[refno] [varchar](50) NULL,
	[pcode] [varchar](50) NULL,
	[qty] [int] NULL,
	[sdate] [datetime] NULL,
 CONSTRAINT [PK_tbStockIn] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwStockIn]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwStockIn]
AS
SELECT dbo.tbStockIn.id, dbo.tbStockIn.refno, dbo.tbStockIn.pcode, dbo.tbProduct.item, dbo.tbStockIn.qty, dbo.tbStockIn.sdate
FROM   dbo.tbProduct INNER JOIN
             dbo.tbStockIn ON dbo.tbProduct.pcode = dbo.tbStockIn.pcode
GO
/****** Object:  Table [dbo].[tbCart]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[transno] [varchar](50) NULL,
	[pcode] [varchar](50) NULL,
	[price] [decimal](18, 2) NULL,
	[qty] [int] NULL,
	[disc_percent] [decimal](18, 2) NULL,
	[disc] [decimal](18, 2) NULL,
	[total] [decimal](18, 2) NULL,
	[sdate] [date] NULL,
	[status] [varchar](50) NULL,
	[cashier] [varchar](50) NULL,
 CONSTRAINT [PK_tbCart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbCategory]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbUser]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUser](
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[role] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[isactive] [varchar](50) NULL,
 CONSTRAINT [PK_tbUser] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbCart] ON 

INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (29, N'202507191002', N'P005', CAST(6300.00 AS Decimal(18, 2)), 5, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(31500.00 AS Decimal(18, 2)), CAST(N'2025-07-19' AS Date), N'Pending', N'Username')
INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (30, N'202507201001', N'P002', CAST(1000.00 AS Decimal(18, 2)), 3, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(3000.00 AS Decimal(18, 2)), CAST(N'2025-07-20' AS Date), N'Pending', N'Username')
INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (31, N'202507201002', N'P003', CAST(18700.00 AS Decimal(18, 2)), 2, CAST(10.00 AS Decimal(18, 2)), CAST(3740.00 AS Decimal(18, 2)), CAST(33660.00 AS Decimal(18, 2)), CAST(N'2025-07-20' AS Date), N'Pending', N'Username')
INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (32, N'202507201002', N'P005', CAST(6300.00 AS Decimal(18, 2)), 4, CAST(5.00 AS Decimal(18, 2)), CAST(1260.00 AS Decimal(18, 2)), CAST(23940.00 AS Decimal(18, 2)), CAST(N'2025-07-20' AS Date), N'Pending', N'Username')
INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (33, N'202507211001', N'P001', CAST(2000.00 AS Decimal(18, 2)), 3, CAST(10.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(5400.00 AS Decimal(18, 2)), CAST(N'2025-07-21' AS Date), N'Pending', N'Username')
INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (34, N'202507211001', N'P006', CAST(1500.00 AS Decimal(18, 2)), 3, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(4500.00 AS Decimal(18, 2)), CAST(N'2025-07-21' AS Date), N'Pending', N'Username')
INSERT [dbo].[tbCart] ([id], [transno], [pcode], [price], [qty], [disc_percent], [disc], [total], [sdate], [status], [cashier]) VALUES (35, N'202507211001', N'P004', CAST(1500.00 AS Decimal(18, 2)), 4, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(6000.00 AS Decimal(18, 2)), CAST(N'2025-07-21' AS Date), N'Pending', N'Username')
SET IDENTITY_INSERT [dbo].[tbCart] OFF
GO
SET IDENTITY_INSERT [dbo].[tbCategory] ON 

INSERT [dbo].[tbCategory] ([id], [category]) VALUES (1, N'Fruits')
INSERT [dbo].[tbCategory] ([id], [category]) VALUES (2, N'Meat')
INSERT [dbo].[tbCategory] ([id], [category]) VALUES (4, N'Vegetable')
SET IDENTITY_INSERT [dbo].[tbCategory] OFF
GO
INSERT [dbo].[tbProduct] ([pcode], [barcode], [item], [cid], [price], [qty], [reorder]) VALUES (N'P001', N'683023', N'Apple', 1, CAST(2000.00 AS Decimal(18, 2)), 30, 50)
INSERT [dbo].[tbProduct] ([pcode], [barcode], [item], [cid], [price], [qty], [reorder]) VALUES (N'P002', N'786546', N'Mango', 1, CAST(1000.00 AS Decimal(18, 2)), 35, 40)
INSERT [dbo].[tbProduct] ([pcode], [barcode], [item], [cid], [price], [qty], [reorder]) VALUES (N'P003', N'947292', N'Frozen Pork Chop 300G', 2, CAST(18700.00 AS Decimal(18, 2)), 30, 35)
INSERT [dbo].[tbProduct] ([pcode], [barcode], [item], [cid], [price], [qty], [reorder]) VALUES (N'P004', N'792647', N'Banana', 1, CAST(1500.00 AS Decimal(18, 2)), 50, 30)
INSERT [dbo].[tbProduct] ([pcode], [barcode], [item], [cid], [price], [qty], [reorder]) VALUES (N'P005', N'230218', N'Fresh Chicken Drumettes (300-350G)', 2, CAST(6300.00 AS Decimal(18, 2)), 35, 20)
INSERT [dbo].[tbProduct] ([pcode], [barcode], [item], [cid], [price], [qty], [reorder]) VALUES (N'P006', N'802719', N'Onion 1000G (Small)', 4, CAST(1500.00 AS Decimal(18, 2)), 60, 60)
GO
SET IDENTITY_INSERT [dbo].[tbStockIn] ON 

INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (1, N'268965502', N'P001', 0, CAST(N'2025-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (2, N'909069888', N'P002', 0, CAST(N'2025-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (3, N'678640458', N'P003', 0, CAST(N'2025-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (7, N'824828931', N'P004', 0, CAST(N'2025-07-17T16:34:11.180' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (8, N'824828931', N'P001', 0, CAST(N'2025-07-17T16:34:11.180' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (9, N'824828931', N'P002', 0, CAST(N'2025-07-17T16:34:11.180' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (10, N'824828931', N'P003', 0, CAST(N'2025-07-17T16:34:11.180' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (11, N'1918365269', N'P003', 0, CAST(N'2025-07-17T16:50:46.300' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (12, N'1146266160', N'P002', 0, CAST(N'2025-07-17T16:52:52.557' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (13, N'833545291', N'P002', 0, CAST(N'2025-07-17T16:56:08.343' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (14, N'1030791917', N'P001', 30, CAST(N'2025-07-17T17:06:01.253' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (18, N'596299072', N'P002', 35, CAST(N'2025-07-17T17:06:25.700' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (19, N'596299072', N'P003', 20, CAST(N'2025-07-17T17:06:25.700' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (20, N'596299072', N'P004', 50, CAST(N'2025-07-17T17:06:25.700' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (21, N'1065977611', N'P003', 5, CAST(N'2025-07-17T17:35:14.760' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (22, N'687861214', N'P005', 35, CAST(N'2025-07-19T15:24:02.553' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (23, N'1005776408', N'P006', 60, CAST(N'2025-07-19T15:24:22.037' AS DateTime))
INSERT [dbo].[tbStockIn] ([id], [refno], [pcode], [qty], [sdate]) VALUES (24, N'513322277', N'P003', 5, CAST(N'2025-07-21T11:47:50.680' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbStockIn] OFF
GO
INSERT [dbo].[tbUser] ([username], [password], [role], [name], [isactive]) VALUES (N'Cashier', N'1234', N'Cashier', N'Justin Bone', N'True')
INSERT [dbo].[tbUser] ([username], [password], [role], [name], [isactive]) VALUES (N'KoPyae', N'0000', N'Adminstrator', N'Pyae Phyo', N'True')
GO
ALTER TABLE [dbo].[tbCart] ADD  CONSTRAINT [DF_tbCart_qty]  DEFAULT ((0)) FOR [qty]
GO
ALTER TABLE [dbo].[tbCart] ADD  CONSTRAINT [DF_tbCart_disc_percent]  DEFAULT ((0)) FOR [disc_percent]
GO
ALTER TABLE [dbo].[tbCart] ADD  CONSTRAINT [DF_tbCart_disc]  DEFAULT ((0)) FOR [disc]
GO
ALTER TABLE [dbo].[tbCart] ADD  CONSTRAINT [DF_tbCart_status]  DEFAULT ('Pending') FOR [status]
GO
ALTER TABLE [dbo].[tbProduct] ADD  CONSTRAINT [DF_tbProduct_qty]  DEFAULT ((0)) FOR [qty]
GO
ALTER TABLE [dbo].[tbStockIn] ADD  CONSTRAINT [DF_tbStockIn_qty]  DEFAULT ((0)) FOR [qty]
GO
ALTER TABLE [dbo].[tbUser] ADD  CONSTRAINT [DF_tbUser_isactive]  DEFAULT ('True') FOR [isactive]
GO
ALTER TABLE [dbo].[tbCart]  WITH CHECK ADD  CONSTRAINT [FK_tbCart_tbProduct1] FOREIGN KEY([pcode])
REFERENCES [dbo].[tbProduct] ([pcode])
GO
ALTER TABLE [dbo].[tbCart] CHECK CONSTRAINT [FK_tbCart_tbProduct1]
GO
ALTER TABLE [dbo].[tbProduct]  WITH CHECK ADD  CONSTRAINT [FK_tbProduct_tbCategory1] FOREIGN KEY([cid])
REFERENCES [dbo].[tbCategory] ([id])
GO
ALTER TABLE [dbo].[tbProduct] CHECK CONSTRAINT [FK_tbProduct_tbCategory1]
GO
ALTER TABLE [dbo].[tbStockIn]  WITH CHECK ADD  CONSTRAINT [FK_tbStockIn_tbProduct] FOREIGN KEY([pcode])
REFERENCES [dbo].[tbProduct] ([pcode])
GO
ALTER TABLE [dbo].[tbStockIn] CHECK CONSTRAINT [FK_tbStockIn_tbProduct]
GO
/****** Object:  Trigger [dbo].[ComputTotal]    Script Date: 7/21/2025 12:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[ComputTotal]
   ON  [dbo].[tbCart] 
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update tbCart SET disc = ((price*qty) * disc_percent * 0.01)
	update tbCart SET total = (price*qty)-disc

    -- Insert statements for trigger here

END
GO
ALTER TABLE [dbo].[tbCart] ENABLE TRIGGER [ComputTotal]
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
         Begin Table = "tbProduct"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbStockIn"
            Begin Extent = 
               Top = 9
               Left = 336
               Bottom = 206
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 1
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
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwStockIn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwStockIn'
GO
USE [master]
GO
ALTER DATABASE [DBMarket] SET  READ_WRITE 
GO
