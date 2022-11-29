
CREATE PROCEDURE [dbo].[GetUser]
	@AccountId INT,
	@Login NVARCHAR(32) OUTPUT,
	@FirstName NVARCHAR(32) OUTPUT,
	@LastName NVARCHAR(32) OUTPUT,
	@Email NVARCHAR(32) OUTPUT
AS
BEGIN
	SELECT 
			@Login = Accounts.Login,
			@FirstName = ProfilesV.FirstName,
			@LastName = ProfilesV.LastName,
			@Email = ProfilesV.Email
	FROM 
	Accounts
	INNER JOIN
	Profiles AS ProfilesV ON Accounts.ProfileId = ProfilesV.ID
	WHERE Accounts.ID = @AccountId

END;