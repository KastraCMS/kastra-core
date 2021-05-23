using Kastra.Admin.Core.Models;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Signin the user.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>Login result</returns>
        Task<LoginResult> Login(LoginModel loginModel);

        /// <summary>
        /// Logout the user.
        /// </summary>
        /// <returns></returns>
        Task Logout();
    }
}
