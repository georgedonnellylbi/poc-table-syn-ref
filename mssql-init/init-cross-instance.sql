-- Create common database on instance COMMON_INSTANCE
CREATE DATABASE [common];
GO
USE [common];
GO
CREATE TABLE dbo.Clubs (
    ClubId INT IDENTITY(1,1) PRIMARY KEY,
    CanonicalName NVARCHAR(100) NOT NULL UNIQUE
);
GO
CREATE INDEX IX_Clubs_CanonicalName ON dbo.Clubs(CanonicalName);
GO
INSERT INTO dbo.Clubs (CanonicalName) VALUES (N'Chess Club');
INSERT INTO dbo.Clubs (CanonicalName) VALUES (N'Film Society');
INSERT INTO dbo.Clubs (CanonicalName) VALUES (N'Book Club');
GO

-- Create footage database on instance FOOTAGE_INSTANCE
CREATE DATABASE [footage];
GO
USE [footage];
GO
CREATE TABLE dbo.Film (
    FilmId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    ClubId INT NOT NULL
);
GO
INSERT INTO dbo.Film (Title, ClubId) VALUES (N'Casablanca', 1);
INSERT INTO dbo.Film (Title, ClubId) VALUES (N'Inception', 2);
INSERT INTO dbo.Film (Title, ClubId) VALUES (N'The Great Gatsby', 3);
GO
ALTER TABLE dbo.Film ADD Active BIT NOT NULL DEFAULT(0);
GO

-- On FOOTAGE_INSTANCE, create a linked server to COMMON_INSTANCE
-- (Run this on FOOTAGE_INSTANCE)
EXEC master.dbo.sp_addlinkedserver 
    @server = N'COMMON_LINKED', 
    @srvproduct = N'SQL Server';
GO

EXEC master.dbo.sp_serveroption 
    @server = N'COMMON_LINKED', 
    @optname = N'data access', 
    @optvalue = N'true';

EXEC master.dbo.sp_addlinkedsrvlogin 
    @rmtsrvname = N'COMMON_LINKED', 
    @useself = N'false', 
    @locallogin = NULL, 
    @rmtuser = N'sa', 
    @rmtpassword = N'YourStrongPassw0rd';

-- Now set the data source:
EXEC sp_setnetname 'COMMON_LINKED', 'sql-common,1433';

-- Create synonym in footage for Clubs table on common via linked server
CREATE SYNONYM dbo.Clubs FOR [COMMON_LINKED].[common].[dbo].[Clubs];
GO


-- Test Connection 
SELECT * FROM [COMMON_LINKED].[common].[dbo].[Clubs];

SELECT * FROM OPENQUERY([COMMON_LINKED], 'SELECT 1')

SELECT * FROM sys.servers WHERE is_linked = 1;