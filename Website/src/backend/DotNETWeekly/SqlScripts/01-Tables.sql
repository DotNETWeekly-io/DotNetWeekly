Create Table dbo.Episode (
	Id int IDENTITY(1, 1) NOT NULL,
	Title nvarchar(100) NOT NULL,
	Content nvarchar(max) NOT NULL,
	CreateTIme Datetime2(7) NOT NULL,
	CONSTRAINT PK_Episode PRIMARY KEY CLUSTERED
	(
		Id ASC
	)
)
GO

CREATE TABLE dbo.Record (
	Id int IDENTITY(1, 1) NOT NULL,
	EpisodeId int NOT NULL,
	Title nvarchar(100) NOT NULL,
	Link nvarchar(max) NOT NULL,
	Content nvarchar(max) NOT NULL,
	Category int NOT NULL,
	CreateTime Datetime2(7) NOT NULL,
	CONSTRAINT PK_Record PRIMARY KEY CLUSTERED
	(
		Id ASC
    )
)
GO

Alter TABLE dbo.Record 
	WITH CHECK ADD CONSTRAINT FK_Record_Episode FOREIGN KEY(EpisodeId)
	references dbo.Episode (Id)
	ON Update CASCADE
	ON Delete CASCADE
Go

Alter Table dbo.Record Check constraint FK_Record_Episode

go
