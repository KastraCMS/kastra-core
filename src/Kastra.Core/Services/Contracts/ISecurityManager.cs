/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kastra.Core.DTO;

namespace Kastra.Core.Services.Contracts
{
    public interface ISecurityManager
    {
        /// <summary>
        /// Gets the permissions list.
        /// </summary>
        /// <returns>The permissions list.</returns>
        Task<IList<PermissionInfo>> GetPermissionsListAsync();

        /// <summary>
        /// Saves the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was saved, <c>false</c> otherwise.</returns>
        /// <param name="permission">Permission.</param>
        Task<bool> SavePermissionAsync(PermissionInfo permission);

        /// <summary>
        /// Deletes the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was deleted, <c>false</c> otherwise.</returns>
        /// <param name="permissionId">Permission identifier.</param>
        Task<bool> DeletePermissionAsync(int permissionId);

        /// <summary>
        /// Gets the permission by identifier.
        /// </summary>
        /// <returns>The permission by identifier.</returns>
        /// <param name="permissionId">Permission identifier.</param>
        Task<PermissionInfo> GetPermissionByIdAsync(int permissionId);

        /// <summary>
        /// Gets the permission by name.
        /// </summary>
        /// <returns>The permission by name.</returns>
        /// <param name="name">Name.</param>
        Task<PermissionInfo> GetPermissionByNameAsync(string name);

        /// <summary>
        /// Gets the module permissions by module identifier.
        /// </summary>
        /// <returns>The module permissions by module identifier.</returns>
        /// <param name="moduleId">Module identifier.</param>
		Task<IList<ModulePermissionInfo>> GetModulePermissionsByModuleIdAsync(int moduleId);

        /// <summary>
        /// Saves the module permission.
        /// </summary>
        /// <returns><c>true</c>, if module permission was saved, <c>false</c> otherwise.</returns>
        /// <param name="modulePermission">Module permission.</param>
		Task<bool> SaveModulePermissionAsync(ModulePermissionInfo modulePermission);

        /// <summary>
        /// Deletes the module permission.
        /// </summary>
        /// <returns><c>true</c>, if module permission was deleted, <c>false</c> otherwise.</returns>
        /// <param name="modulePermissionId">Module permission identifier.</param>
		Task<bool> DeleteModulePermissionAsync(int modulePermissionId);

        /// <summary>
        /// Deletes the module permission.
        /// </summary>
        /// <returns><c>true</c>, if module permission was deleted, <c>false</c> otherwise.</returns>
        /// <param name="moduleId">Module identifier.</param>
        /// <param name="permissionId">Permission identifier.</param>
		Task<bool> DeleteModulePermissionAsync(int moduleId, int permissionId);
    }
}
