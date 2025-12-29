-- Create common and footage databases
CREATE DATABASE [common];
GO
CREATE DATABASE [footage];
GO

-- Create Clubs table in common
USE [common];
GO
CREATE TABLE dbo.Clubs (
    ClubId INT IDENTITY(1,1) PRIMARY KEY,
    CanonicalName NVARCHAR(100) NOT NULL UNIQUE
);
GO
CREATE INDEX IX_Clubs_CanonicalName ON dbo.Clubs(CanonicalName);
GO

USE [footage];
GO
CREATE TABLE dbo.Film (
    FilmId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    ClubId INT NOT NULL
);
GO
CREATE SYNONYM dbo.Clubs FOR [common].[dbo].[Clubs];
GO

-- Insert sample data into common.Clubs
USE [common];
GO
INSERT INTO dbo.Clubs (CanonicalName) VALUES (N'Chess Club');
INSERT INTO dbo.Clubs (CanonicalName) VALUES (N'Film Society');
INSERT INTO dbo.Clubs (CanonicalName) VALUES (N'Book Club');
GO

-- Insert sample data into footage.Film (using valid ClubId values)
USE [footage];
GO
INSERT INTO dbo.Film (Title, ClubId) VALUES (N'Casablanca', 1);
INSERT INTO dbo.Film (Title, ClubId) VALUES (N'Inception', 2);
INSERT INTO dbo.Film (Title, ClubId) VALUES (N'The Great Gatsby', 3);
GO

-- Add Active flag to Film table 100 days later
ALTER TABLE dbo.Film ADD Active BIT NOT NULL DEFAULT(0);
GO
