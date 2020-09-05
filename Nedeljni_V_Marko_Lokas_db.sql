IF DB_ID('BetweenUs') IS NULL
CREATE DATABASE BetweenUs
GO

USE BetweenUs

ALTER DATABASE BetweenUs COLLATE Croatian_CI_AS;
GO

-- Drop Foreign Keys
IF OBJECT_ID('tblFriendRequest', 'U') IS NOT NULL 
	ALTER TABLE tblFriendRequest DROP CONSTRAINT FK_Received_User;
IF OBJECT_ID('tblFriendRequest', 'U') IS NOT NULL 
	ALTER TABLE tblFriendRequest DROP CONSTRAINT FK_Sent_User;
IF OBJECT_ID('tblPost', 'U') IS NOT NULL 
	ALTER TABLE tblPost DROP CONSTRAINT FK_Post_User;
IF OBJECT_ID('tblPostLike', 'U') IS NOT NULL 
	ALTER TABLE tblPostLike DROP CONSTRAINT FK_PostLike_Post;
IF OBJECT_ID('tblPostLike', 'U') IS NOT NULL 
	ALTER TABLE tblPostLike DROP CONSTRAINT FK_PostLike_User;
--===================================================================

-- Drop Primary Keys 
IF OBJECT_ID('tblUser', 'U') IS NOT NULL 
	ALTER TABLE tblUser DROP CONSTRAINT PK_User;
IF OBJECT_ID('tblFriendRequest', 'U') IS NOT NULL 
	ALTER TABLE tblFriendRequest DROP CONSTRAINT PK_FriendRequest;
IF OBJECT_ID('tblPost', 'U') IS NOT NULL 
	ALTER TABLE tblPost DROP CONSTRAINT PK_Post;
IF OBJECT_ID('tblPostLike', 'U') IS NOT NULL 
	ALTER TABLE tblPostLike DROP CONSTRAINT PK_PostLike;
--===================================================================

-- Drop tables
IF OBJECT_ID('tblUser', 'U') IS NOT NULL 
	DROP TABLE tblUser;
IF OBJECT_ID('tblFriendRequest', 'U') IS NOT NULL 
	DROP TABLE tblFriendRequest;
IF OBJECT_ID('tblPost', 'U') IS NOT NULL 
	DROP TABLE tblPost;
IF OBJECT_ID('tblPostLike', 'U') IS NOT NULL 
	DROP TABLE tblPostLike;
--===================================================================

-- Create tables
CREATE TABLE tblUser(
	UserID					int identity(1,1) NOT NULL,
	UserName				nvarchar (50) NOT NULL,
	UserSurname				nvarchar (50) NOT NULL,
	UserUsername			nvarchar (50) NOT NULL UNIQUE,
	UserPassword			nvarchar (50) NOT NULL, 
	DateOfBirth				date NOT NULL,
	VisibleDate				int NOT NULL,
	Gender					nvarchar (20) NOT NULL,
	VisibleGender			int NOT NULL,
	LastSeen				datetime
	)

CREATE TABLE tblFriendRequest(
	FriendRequestID			int identity(1,1) NOT NULL,
	UserSent				int NOT NULL,
	UserReceived			int NOT NULL,
	FriendRequestStatus		int NOT NULL
	)

CREATE TABLE tblPost(
	PostID					int identity(1,1) NOT NULL,
	UserPost				int NOT NULL,
	DateTimePost			datetime NOT NULL,
	VisiblePost				int NOT NULL
	)

CREATE TABLE tblPostLike(
	PostLikeID				int identity(1,1) NOT NULL,
	PostLike				int NOT NULL,
	UserLike				int NOT NULL
	)
--===================================================================

-- Add contraints for PK
ALTER TABLE tblUser 
	ADD CONSTRAINT PK_User 
	PRIMARY KEY (UserID)
ALTER TABLE tblFriendRequest
	ADD CONSTRAINT PK_FriendRequest
	PRIMARY KEY (FriendRequestID)
ALTER TABLE tblPost
	ADD CONSTRAINT PK_Post
	PRIMARY KEY (PostID)
ALTER TABLE tblPostLike
	ADD CONSTRAINT PK_PostLike
	PRIMARY KEY (PostLikeID)
--===================================================================

-- Add contraints for FK
ALTER TABLE tblFriendRequest 
	ADD CONSTRAINT FK_Received_User
	FOREIGN KEY (UserReceived) 
	REFERENCES tblUser (UserID)
ALTER TABLE tblFriendRequest 
	ADD CONSTRAINT FK_Sent_User
	FOREIGN KEY (UserSent) 
	REFERENCES tblUser (UserID)
ALTER TABLE tblPost 
	ADD CONSTRAINT FK_Post_User
	FOREIGN KEY (UserPost) 
	REFERENCES tblUser (UserID)
ALTER TABLE tblPostLike 
	ADD CONSTRAINT FK_PostLike_Post
	FOREIGN KEY (PostLike) 
	REFERENCES tblPost (PostID)
ALTER TABLE tblPostLike
	ADD CONSTRAINT FK_PostLike_User
	FOREIGN KEY (UserLike)
	REFERENCES tblUser(UserID)
--===================================================================