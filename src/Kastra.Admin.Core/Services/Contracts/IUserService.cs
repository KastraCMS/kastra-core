using Kastra.Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Load the user list.
        /// </summary>
        /// <returns></returns>
        Task<IList<UserModel>> LoadUserList(); 

        /// <summary>
        /// Load an user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        Task<UserModel> LoadUser(string userId);

        /// <summary>
        /// Save an user.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<bool> SaveUser(UserModel user);

        /// <summary>
        /// Delete an user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        Task<bool> DeleteUser(Guid userId);
    }
}
