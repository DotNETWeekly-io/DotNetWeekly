Create PROC dbo.Episodes_Get
As
Begin 
	Set NoCount On
	Select e.Id, e.Title, e.Introduction, e.CreateTime
	From dbo.Episode e 
End
Go

Create PROC dbo.Episode_Get_ById
(
	@EpisodeId int
)
As
Begin
	SET NoCount On
	Select e.Id, e.Title, e.Introduction, e.CreateTime, r.Id, r.title, r.Content, r.Category, r.CreateTime
	From dbo.Episode e
		Left Join dbo.Record r On e.Id = r.EpisodeId
End 
Go


Create PROC dbo.Episode_Post
	(
		@Title nvarchar(100),
		@Introduction nvarchar(max),
		@CreateTIme datetime2

	)
As 
Begin
	Set NoCount On
	Insert into dbo.Episode
		(Title, Introduction, CreateTime)
	Values (@Title, @Introduction, @CreateTime)

	Select Scope_Identity() as Id
End
Go

Create PROC dbo.Record_Post
	(
		@EpisodeId int,
		@Title nvarchar(100),
		@Content nvarchar(max),
		@Category int,
		@CreateTime datetime2
	)
As
Begin
	Set NoCount On
	Insert into dbo.Record
		(EpisodeId, Title, Content, Category, CreateTime)
	Values(@EpisodeId, @Title, @Content, @Category, @CateTime)

	Select Scope_Identity() as Id
End
Go