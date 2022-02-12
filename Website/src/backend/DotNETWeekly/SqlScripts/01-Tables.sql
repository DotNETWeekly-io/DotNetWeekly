Create Table dbo.Episode (
	Id int IDENTITY(1, 1) NOT NULL,
	Title nvarchar(256) NOT NULL,
	Content nvarchar(max) NOT NULL,
	CreateTime Datetime2(7) Default(GETUTCDATE()),
	UpdateTime Datetime2(7) Default(GETUTCDATE())
	CONSTRAINT PK_Episode PRIMARY KEY CLUSTERED
	(
		Id ASC
	)
)

EXEC ('
Create Trigger dbo.Episode_updateTime_trigger
ON dbo.Episode
AFTER Update
AS
Update dbo.Episode
SET UpdateTime = GETUTCDATE()
Where Id in (Select Distinct Id from Inserted)
')

GO