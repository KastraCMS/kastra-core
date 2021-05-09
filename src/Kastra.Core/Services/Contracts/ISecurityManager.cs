/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using Kastra.Core.DTO;

namespace Kastra.Core.Services.Contracts
{
    public interface ISecurityManager
    {
        /// <summary>
        /// Gets the permissions list.
        /// </summary>
        /// <returns>The permissions list.</returns>
        IList<PermissionInfo> GetPermissionsList();

        /// <summary>
        /// Saves the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was saved, <c>false</c> otherwise.</returns>
        /// <param name="permission">Permission.</param>
        bool SavePermission(PermissionInfo permission);

        /// <summary>
        /// Deletes the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was deleted, <c>false</c> otherwise.</returns>
        /// <param name="permissionId">Permission identifier.</param>
        bool DeletePermission(int permissionId);

        /// <summary>
        /// Gets the permission by identifier.
        /// </summary>
        /// <returns>The permission by identifier.</returns>
        /// <param name="permissionId">Permission identifier.</param>
        PermissionInfo GetPermissionById(int permissionId);

        /// <summary>
        /// Gets the permission by name.
        /// </summary>
        /// <returns>The permission by name.</returns>
        /// <param name="name">Name.</param>
        PermissionInfo GetPermissionByName(String name);

        /// <summary>
        /// Gets the module permissions by module identifier.
        /// </summary>
        /// <returns>The module permissions by module identifier.</returns>
        /// <param name="moduleId">Module identifier.</param>
		IList<ModulePermissionInfo> GetModulePermissionsByModuleId(int moduleId);

        /// <summary>
        /// Saves the module permission.
        /// </summary>
        /// <returns><c>true</c>, if module permission was saved, <c>false</c> otherwise.</returns>
        /// <param name="modulePermission">Module permission.</param>
		bool SaveModulePermission(ModulePermissionInfo modulePermission);

        /// <summary>
        /// Deletes the module permission.
        /// </summary>
        /// <returns><c>true</c>, if module permission was deleted, <c>false</c> otherwise.</returns>
        /// <param name="modulePermissionId">Module permission identifier.</param>
		bool DeleteModulePermission(int modulePermissionId);

        /// <summary>
        /// Deletes the module permission.
        /// </summary>
        /// <returns><c>true</c>, if module permission was deleted, <c>false</c> otherwise.</returns>
        /// <param name="moduleId">Module identifier.</param>
        /// <param name="permissionId">Permission identifier.</param>
		bool DeleteModulePermission(int moduleId, int permissionId);
    }
}
