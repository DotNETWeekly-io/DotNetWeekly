Create PROC dbo.Episodes_Title_Get
As
Begin
	Set NoCount On
	Select e.Id, e.Title, e.CreateTime, e.Content
	from dbo.Episode e
End
Go

Create PROC dbo.Episode_Content_Get_ById
(
	@EpisodeId int
)
As
Begin
	SET NoCount On
	Select e.Id, e.Title, e.Content
	From dbo.Episode e
	Where e.Id = @EpisodeId
End
Go


Create PROC dbo.Episode_Post
	(
		@Title nvarchar(256),
		@Content nvarchar(max)
	)
As 
Begin
	Set NoCount On
	Begin
		Insert into dbo.Episode
			(Title, Content)
		Values (@Title, @Content)
		Select Scope_Identity() as Id
	End
End
Go

Create PROC dbo.Episode_Put
	(
		@Id int,
		@Title nvarchar(256),
		@Content nvarchar(max)
	)
AS
Begin
	SET NoCount On
	Begin
		Update dbo.Episode
		SET
			Title = @Title,
			Content = @Content
		where Id = @Id
	End
End
GO

Create Proc dbo.Episode_Delete
(
	@Id int
)
AS
Begin
	SET NoCount On
	Begin
		Delete 
		From dbo.Episode
		Where Id = @Id
	End
End
GO