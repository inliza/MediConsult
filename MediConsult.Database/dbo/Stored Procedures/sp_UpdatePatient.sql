




CREATE PROCEDURE [dbo].[sp_UpdatePatient]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address nvarchar(50),
	@Gender nchar(100),
	@Document nvarchar(13),
    @Phone nvarchar(11)
AS
	BEGIN
    UPDATE [dbo].[Patient]
    SET 
        [FistName] = @FirstName,
        [LastName] = @LastName,
        [Address] = @Address,
        [Gender] = @Gender,
        [Document] = @Document,
        [Phone] = @Phone
    WHERE [Id] = @Id
END