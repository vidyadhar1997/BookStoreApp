create table UserInformation
(
	 UserId int IDENTITY(1,1) PRIMARY KEY,
	 FirstName varchar(255),
	 LastName varchar(255),
	 UserRole varchar(255),
	 Email varchar(255),
	 Password varchar(255),
	 Address varchar(255),
	 City varchar(255),
	 PhoneNumber varchar(255),
	 CreatedDate datetime
 )
 select * from [dbo].[UserInformation]
 drop table [dbo].[UserInformation]
 drop procedure [dbo].[SpAddUserDetails]