

using System.Threading.Tasks;

namespace CoreApiRegister.Features.Authentication
{
    public interface IAuthenticationServices
    {
        Task<AuthenticationResponseModel> Login(AuthenticationRequestModel model);
        Task Logout();

    }
}
