using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreApiRegister.Models.Identity;
using Microsoft.AspNetCore.Identity;
using CoreApiRegister.Data.Models;
using Microsoft.Extensions.Options;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CoreApiRegister.Controllers
{

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<AppSettings> _appsettings;

        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> options)
        {
            this._userManager = userManager;
            this._appsettings = options;
        }

 
        [Route(nameof(Register))]
        public async Task <ActionResult> Register(RegisterUserRequestModel vm)
        {
            User user = new User
            {
                Email = vm.Email,
                UserName = vm.Username,
            };
            var result = await this._userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [Route(nameof(Login))]
        public async Task<ActionResult<object>> Login(LoginUserRequestModel vm)
        {

            var user = await _userManager.FindByNameAsync(vm.UserName);
            if(user == null)
            {
                return Unauthorized();
            }
            var ispasswordvalid = await _userManager.CheckPasswordAsync(user,vm.Password);

            if (!ispasswordvalid)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return new 
            {               
               token = encryptedToken
            };

        }


        [Route(nameof(GetAllUser))]
        public async Task<ActionResult> GetAllUser()
        {

            var result = await _userManager.FindByNameAsync("endri");
            if (result !=null)
            {
                return Ok(result.Email);
            }
            return BadRequest();
        }

    }

  
}
