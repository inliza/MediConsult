
CREATE PROCEDURE [dbo].[sp_CreateUser]
	@UserName nvarchar(50),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Password nvarchar(max)
AS
	BEGIN
		INSERT INTO [dbo].[User](
		[UserName],
		[FirstName],
		[LastName],
		[Password],
		[CreatedAt],
		[IsActive])
		VALUES(
		@UserName,
		@FirstName,
		@LastName,
		@Password,
		GETDATE(),
		0
		)
END