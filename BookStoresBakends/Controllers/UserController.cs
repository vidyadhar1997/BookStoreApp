using BusinessLayer.IBusiness;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
