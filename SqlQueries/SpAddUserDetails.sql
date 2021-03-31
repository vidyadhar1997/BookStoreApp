create procedure SpAddUserDetails
(
@FirstName varchar(255),
@LastName varchar(255),
@Email varchar(255),
@Password varchar(255),
@Address varchar(255),
@City varchar(255),
@PhoneNumber varchar(255),
@CreatedDate datetime
)
as
begin
	insert into [dbo].[UserInformation] (FirstName,LastName,UserRole,Email,Password,Address,City,PhoneNumber, CreatedDate)
	values (@FirstName, @LastName  ,'Customer', @Email , @Password , @Address , @City , @PhoneNumber , @CreatedDate);
	select * from UserInformation
end
