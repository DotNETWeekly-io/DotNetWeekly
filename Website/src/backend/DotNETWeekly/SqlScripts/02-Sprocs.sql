Create PROC dbo.Episodes_Title_Get
As
Begin
	Set NoCount On
	Select e.Id, e.Title, e.CreateTime
	from dbo.Episode e
End
Go

Create PROC dbo.Episode_Records_Get_ById
(
	@EpisodeId int
)
As
Begin
	SET NoCount On
	Select e.Id, e.Title, e.Introduction, e.CreateTime, r.Id, r.Title, r.Link, r.Content, r.Category, r.CreateTime, r.EpisodeId
	From dbo.Episode e
		Left Join dbo.Record r On e.Id = r.EpisodeId 
	Where e.Id = @EpisodeId
End 
Go

Create PROC dbo.Episode_Content_Get_ById
(
	@EpisodeId int
)
As
Begin
	SET NoCount On
	Select e.Id, e.Title, e.Content, e.CreateTime
	From dbo.Episode e
	where e.Id = @EpisodeId
End
Go


Create PROC dbo.Episode_Post
	(
		@Id int,
		@Title nvarchar(100),
		@Content nvarchar(max),
		@CreateTime datetime2
	)
As 
Begin
	Set NoCount On
	IF NOT EXISTS(Select * From dob.Episode where Id = @Id)
	Begin
		Insert into dbo.Episode
			(Title, Introduction, Content, CreateTime)
		Values (@Title, @Introduction, @Content, @CreateTime)
		Select Scope_Identity() as Id
	End
	Else 
	Begin 
		Update dbo.Episode
		SET
		    Title = @Title,
			Content = @Content,
			CreateTime = GETUTCDATE()
		Where Id = @id
	end
End
Go

Create PROC dbo.Record_Post
	(
		@EpisodeId int,
		@Title nvarchar(100),
		@Link nvarchar(max),
		@Content nvarchar(max),
		@Category int,
		@CreateTime datetime2
	)
As
Begin
	Set NoCount On
	Insert into dbo.Record
		(EpisodeId, Title, Link, Content, Category, CreateTime)
	Values(@EpisodeId, @Title, @Link, @Content, @Category, @CreateTime)

	Select Scope_Identity() as Id
End
Go