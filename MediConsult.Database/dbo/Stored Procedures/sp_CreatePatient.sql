



CREATE PROCEDURE [dbo].[sp_CreatePatient]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address nvarchar(50),
	@Gender nchar(100),
	@Document nvarchar(13),
    @Phone nvarchar(11)
AS
	BEGIN
		INSERT INTO [dbo].Patient(
		[FistName],
		[LastName],
		[Address],
		[Gender],
		[Document],
		[Phone],
		[CreatedAt],
		[IsDeleted])
		VALUES(
		@FirstName,
		@LastName,
		@Address,
		@Gender,
		@Document,
		@Phone,
		GETDATE(),
		0
		)
END