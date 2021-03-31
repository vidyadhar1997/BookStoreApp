using BusinessLayer.IBusiness;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoresBakends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness UserB;

        private readonly IConfiguration _configuration;

        public UserController(IUserBusiness UserB, IConfiguration _configuration)
        {
            this.UserB = UserB;
            this._configuration = _configuration;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult UserRegistration(UserRegistration user)
        {
            try
            {
                var data = UserB.Registration(user);
                if (data != null)
                {
                    return Ok(new { success = true, Message = "registration successfull", Data = data });
                }
                else
                {
                    return Ok(new { success = false, Message = "registration failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(UserLogin login)
        {
            try
            {
                UserDetails data = UserB.Login(login);

                bool success = false;
                string message;
                UserDetails DATA;

                UserDetails Data = new UserDetails()
                {
                    UserId = data.UserId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    UserRole = data.UserRole,
                    Email = data.Email,
                    Address = data.Address,
                    City = data.City,
                    PhoneNumber = data.PhoneNumber
                };

                if (data.Email != null)
                {
                    string JsonToken = CreateToken(data, "AuthenticateUserRole");
                    success = true;
                    message = "Login Successfully";
                    DATA = Data;
                    return Ok(new { success, message, DATA, JsonToken });
                }
                else
                {
                      message = "Enter Valid Email & Password";
                      return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        private string CreateToken(UserDetails responseData, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, responseData.UserRole));
                claims.Add(new Claim("Email", responseData.Email.ToString()));
                claims.Add(new Claim("UserId", responseData.UserId.ToString()));
                claims.Add(new Claim("TokenType", type));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
