using Kastra.Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IRoleService
    {
        Task<RoleModel> LoadRole(Guid roleId);

        Task<IList<RoleModel>> LoadRoleList();

        Task<bool> SaveRole(RoleModel role);

        Task<bool> DeleteRole(Guid roleId);
    }
}
