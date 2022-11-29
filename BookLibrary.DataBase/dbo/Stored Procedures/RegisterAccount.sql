
CREATE PROCEDURE [dbo].[RegisterAccount]
	@Login NVARCHAR (64),
	@Password NVARCHAR (32),
	@FirstName NVARCHAR (64),
	@LastName NVARCHAR (64),
	@Email NVARCHAR (32),
	@Result INT OUTPUT
AS
	IF NOT EXISTS (SELECT * FROM Accounts 
                   WHERE Login = @Login
				   )
	BEGIN
		DECLARE @ProfileId INT;
		DECLARE @AccountId INT;
		SET @ProfileId = NULL
		SELECT TOP 1 @ProfileId = ID
		FROM Profiles
		WHERE FirstName = @FirstName 
			AND LastName = @LastName
			AND Email = @Email
		ORDER BY ID ASC;
		IF @ProfileId IS NOT NULL
		BEGIN
			INSERT INTO Accounts (Login, Password, ProfileId)
			VALUES (@Login, @Password, @ProfileId);
			SET @AccountId = IDENT_CURRENT('Accounts');
		END
		ELSE
		BEGIN
			INSERT INTO Profiles (FirstName, LastName, Email)
			VALUES (@FirstName, @LastName, @Email);
			SET @ProfileId = IDENT_CURRENT('Profiles');
			INSERT INTO Accounts (Login, Password, ProfileId)
			VALUES (@Login, @Password, @ProfileId);
			SET @AccountId = IDENT_CURRENT('Accounts');
		END
		SET @Result = @AccountId
	END 
	ELSE
	BEGIN
		SET @Result = -1
	END
RETURN 0