GO
CREATE PROCEDURE CreateUser
    @Id BIGINT,
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255),
    @PasswordSalt VARBINARY(255),
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @BirthDate DATE,
    @Telephone VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Users (id, email, passwordHash, passwordSalt)
    VALUES (@Id, @Email, @PasswordHash, @PasswordSalt);

    INSERT INTO Consumers (id, firstName, lastName, birthDate, telephone)
    VALUES (@Id, @FirstName, @LastName, @BirthDate, @Telephone);
END;

DROP TYPE IF EXISTS UserConsumerType;

CREATE TYPE UserConsumerType AS TABLE
(
    Id BIGINT,
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(255),
    PasswordSalt VARBINARY(255),
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    BirthDate DATE,
    Telephone VARCHAR(20)
);
GO
CREATE PROCEDURE CreateUsersBulk
    @UserData UserConsumerType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Umetanje u tabelu Users koristeći prosleđene Id vrednosti
    INSERT INTO Users (id, email, passwordHash, passwordSalt)
    SELECT Id, Email, PasswordHash, PasswordSalt
    FROM @UserData;

    -- Umetanje u tabelu Consumers koristeći prosleđene Id vrednosti
    INSERT INTO Consumers (id, firstName, lastName, birthDate, telephone)
    SELECT Id, FirstName, LastName, BirthDate, Telephone
    FROM @UserData;
END;

DROP TYPE IF EXISTS ConsumerFriendType

CREATE TYPE ConsumerFriendType AS TABLE
(
    consumerId BIGINT,
    friendId BIGINT,
    friendshipLevel INT CHECK (FriendshipLevel BETWEEN 1 AND 10),
    establishedDate DATE
);
GO
CREATE PROCEDURE CreateConsumerFriendsBulk
    @FriendsData ConsumerFriendType READONLY
AS
BEGIN
    INSERT INTO ConsumerFriends (consumerId,friendId,friendshipLevel,establishedDate)
    SELECT consumerId,friendId, friendshipLevel
    FROM @FriendsData;
END;

GO
-- DELETE STORED PROCEDURE
CREATE PROCEDURE DeleteConsumerFriendById
    @ConsumerId BIGINT,
    @FriendId BIGINT
AS
BEGIN
    DELETE FROM ConsumerFriends WHERE consumerId=@ConsumerId AND friendId=@FriendId;
END;

GO
CREATE PROCEDURE DeleteConsumerFriendByEstablishedDateAndLevel
    @Date DATE,
    @FriendshipLevel INT
AS
BEGIN
    DELETE FROM ConsumerFriends WHERE establishedDate< @Date AND friendshipLevel>=@FriendshipLevel;
END;

GO
CREATE PROCEDURE UpdateConsumerFriendFriendshipLevel
    @ConsumerId BIGINT,
    @FriendId BIGINT,
    @NewFriendshipLevel BIGINT
AS
BEGIN
    UPDATE ConsumersFriends
    SET friendshipLevel=@NewFriendshipLevel
    WHERE consumerId = @ConsumerId AND friendId = @FriendId;
END;

GO
CREATE PROCEDURE UpdateConsumerFriendByEmail
    @Email NVARCHAR(50)
AS
BEGIN
    UPDATE ConsumersFriends
    SET friendshipLevel= friendshipLevel - 2
    WHERE consumerId IN (SELECT id FROM Consumers c WHERE c.id IN(SELECT id FROM Users u WHERE u.email=@Email))
END;