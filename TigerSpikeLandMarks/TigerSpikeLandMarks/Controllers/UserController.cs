using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TigerSpikeLandMarks.Entities;
using TigerSpikeLandMarks.Entities.DTOs;
using TigerSpikeLandMarks.Managers.UserManager;

namespace TigerSpikeLandMarks.Controllers
{
    public class UserController : Controller
    {
        private IUserManager _userManager;
        private readonly AppSettings _appSettings;
        IHttpContextAccessor _httpContextAccessor;
        private string user;

        public UserController(IUserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("user/login")]
        public IActionResult RequestToken([FromBody]TokenRequest request)
        {
            User user = _userManager.Authenticate(request.Username, request.Password);
            if (user != null)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AFC2343FDFASXC45234ADFDSFVSDF1"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var token = new JwtSecurityToken(
                    issuer: "landmarks.tigerspike.com",
                    audience: "landmarks.tigerspike.com",
                    expires: DateTime.Now.AddMinutes(30),
                    claims: claims,
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }

        [AllowAnonymous]
        [HttpPost("user/register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            //var user = _mapper.Map<User>(userDto);
            User user = new User();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Username = userDto.Username;

            try
            {
                // save 
                _userManager.Create(user, userDto.Password);
                return Ok(new DefaultResponse
                {
                    message = "User has been added",
                    status = "success"
                })
                ;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("user/getProfile")]
        public IActionResult getUserProfile()
        {
            try
            {
                user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                // save 
                return Ok(_userManager.GetByUserName(user));
                
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }




        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    class UserRequestDTO
    {

    }
}
