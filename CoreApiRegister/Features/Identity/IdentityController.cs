using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CoreApiRegister.Data.Models;
using Microsoft.Extensions.Options;
using CoreApiRegister.Controllers;
using CoreApiRegister.Features.Identity.Models;

namespace CoreApiRegister.Features.Identity
{

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService identityService;
        private readonly IOptions<AppSettings> _appsettings;

        public IdentityController(UserManager<User> userManager,
            IIdentityService identityService
            , IOptions<AppSettings> options)
        {
            this._userManager = userManager;
            this.identityService = identityService;
            this._appsettings = options;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel vm)
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
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginUserRequestModel vm)
        {

            var user = await _userManager.FindByNameAsync(vm.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            var ispasswordvalid = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (!ispasswordvalid)
            {
                return Unauthorized();
            }

            var token = identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                _appsettings.Value.Secret
                );
            return new LoginResponseModel
            {
                token = token
            };

        }

    }


}
