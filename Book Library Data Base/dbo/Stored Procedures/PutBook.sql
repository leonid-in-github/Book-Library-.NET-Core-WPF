
CREATE PROCEDURE [dbo].[PutBook]
	@AccountId INT,
	@BookId INT
AS
BEGIN
	IF EXISTS (SELECT ID FROM Accounts WHERE ID = @AccountId)
	BEGIN
		IF EXISTS (SELECT ID FROM Books WHERE ID = @BookId)
		BEGIN
			DECLARE @Availability BIT;
			SELECT @Availability = Availability FROM Books WHERE ID = @BookId;
			IF (@Availability <> 1)
			BEGIN
				INSERT INTO BookTracking (BookId, AccountId, ActionTime, Action)
				VALUES (@BookId, @AccountId, CURRENT_TIMESTAMP, 0);
				UPDATE Books
				SET Availability = 1
				WHERE ID = @BookID
			END
		END
	END
	UPDATE Books
	SET Availability = 1
	WHERE ID = @BookID
END;