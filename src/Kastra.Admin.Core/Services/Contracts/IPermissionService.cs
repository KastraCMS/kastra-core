using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IPermissionService
    {
        /// <summary>
        /// Load the permission list.
        /// </summary>
        /// <returns></returns>
        Task<IList<PermissionModel>> LoadPermissionList();

        /// <summary>
        /// Save a permission.
        /// </summary>
        /// <param name="permission">Permission</param>
        /// <returns></returns>
        Task<bool> SavePermission(PermissionModel permission);

        /// <summary>
        /// Delete a permission.
        /// </summary>
        /// <param name="permissionId">Permission Id</param>
        /// <returns></returns>
        Task<bool> DeletePermission(int permissionId);
    }
}
