
create procedure [dbo].[SpAddUserLogin]
(

@Email varchar(255),
@Password varchar(255)
)
as
begin
	SELECT  UserId,FirstName,LastName,UserRole,Email,Address,City,PhoneNumber FROM [dbo].[UserInformation] WHERE Email=@Email And Password=@Password
end
