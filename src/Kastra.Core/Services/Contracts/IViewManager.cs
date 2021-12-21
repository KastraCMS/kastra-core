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
    public interface IViewManager
    {
        #region Page

        /// <summary>
        /// Gets the pages list.
        /// </summary>
        /// <returns>The pages list.</returns>
        Task<IList<PageInfo>> GetPagesListAsync();

        /// <summary>
        /// Saves the page.
        /// </summary>
        /// <returns><c>true</c>, if page was saved, <c>false</c> otherwise.</returns>
        /// <param name="page">Page.</param>
        Task<bool> SavePageAsync(PageInfo page);

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="pageID">Page identifier.</param>
        /// <param name="getAll">If set to <c>true</c> get all.</param>
        Task<PageInfo> GetPageAsync(int pageID, bool getAll = false);

        /// <summary>
        /// Gets the page by key.
        /// </summary>
        /// <returns>The page by key.</returns>
        /// <param name="keyName">Key name.</param>
        /// <param name="getAll">If set to <c>true</c> get all.</param>
        Task<PageInfo> GetPageByKeyAsync(String keyName, bool getAll = false);

        /// <summary>
        /// Deletes the page.
        /// </summary>
        /// <returns><c>true</c>, if page was deleted, <c>false</c> otherwise.</returns>
        /// <param name="pageID">Page identifier.</param>
        Task<bool> DeletePageAsync(int pageID);

        #endregion

        #region Page template

        /// <summary>
        /// Gets the page templates list.
        /// </summary>
        /// <returns>The page templates list.</returns>
        Task<IList<TemplateInfo>> GetPageTemplatesListAsync();

        /// <summary>
        /// Gets the page template.
        /// </summary>
        /// <returns>The page template.</returns>
        /// <param name="pageTemplateID">Page template identifier.</param>
        Task<TemplateInfo> GetPageTemplateAsync(int pageTemplateID);

        /// <summary>
        /// Saves the page template.
        /// </summary>
        /// <returns><c>true</c>, if page template was saved, <c>false</c> otherwise.</returns>
        /// <param name="template">Template.</param>
        Task<bool> SavePageTemplateAsync(TemplateInfo template);

        /// <summary>
        /// Deletes the page template.
        /// </summary>
        /// <returns><c>true</c>, if page template was deleted, <c>false</c> otherwise.</returns>
        /// <param name="pageTemplateID">Page template identifier.</param>
        Task<bool> DeletePageTemplateAsync(int pageTemplateID);

        #endregion

        #region Placeholders

        /// <summary>
        /// Gets the places list.
        /// </summary>
        /// <returns>The places list.</returns>
        /// <param name="includeModules">If set to <c>true</c> include modules.</param>
        Task<IList<PlaceInfo>> GetPlacesListAsync(bool includeModules = false);

        /// <summary>
        /// Gets the place.
        /// </summary>
        /// <returns>The place.</returns>
        /// <param name="placeID">Place identifier.</param>
        Task<PlaceInfo> GetPlaceAsync(int placeID);

        /// <summary>
        /// Saves the place.
        /// </summary>
        /// <returns><c>true</c>, if place was saved, <c>false</c> otherwise.</returns>
        /// <param name="place">Place.</param>
        Task<bool> SavePlaceAsync(PlaceInfo place);

        /// <summary>
        /// Deletes the place.
        /// </summary>
        /// <returns><c>true</c>, if place was deleted, <c>false</c> otherwise.</returns>
        /// <param name="placeID">Place identifier.</param>
        Task<bool> DeletePlaceAsync(int placeID);

        #endregion

        #region Module definition

        /// <summary>
        /// Gets the module def.
        /// </summary>
        /// <returns>The module def.</returns>
        /// <param name="moduleDefID">Module def identifier.</param>
        /// <param name="getModuleControls">If set to <c>true</c> get module controls.</param>
        Task<ModuleDefinitionInfo> GetModuleDefAsync(int moduleDefID, bool getModuleControls = false);

        /// <summary>
        /// Gets the module defs list.
        /// </summary>
        /// <returns>The module defs list.</returns>
        Task<IList<ModuleDefinitionInfo>> GetModuleDefsListAsync();

        /// <summary>
        /// Saves the module def.
        /// </summary>
        /// <returns><c>true</c>, if module def was saved, <c>false</c> otherwise.</returns>
        /// <param name="moduleDefinition">Module definition.</param>
        Task<bool> SaveModuleDefAsync(ModuleDefinitionInfo moduleDefinition);

        /// <summary>
        /// Deletes the module def.
        /// </summary>
        /// <returns><c>true</c>, if module def was deleted, <c>false</c> otherwise.</returns>
        /// <param name="moduleDefID">Module def identifier.</param>
        Task<bool> DeleteModuleDefAsync(int moduleDefID);

        #endregion

        #region Module

        /// <summary>
        /// Gets the module.
        /// </summary>
        /// <returns>The module.</returns>
        /// <param name="moduleID">Module identifier.</param>
        /// <param name="getModuleDef">If set to <c>true</c> get module def.</param>
        /// <param name="getPlace">If set to <c>true</c> get place.</param>
        Task<ModuleInfo> GetModuleAsync(int moduleID, bool getModuleDef = false, bool getPlace = false);

        /// <summary>
        /// Gets the modules list.
        /// </summary>
        /// <returns>The modules list.</returns>
        /// <param name="includeModuleControls">If set to <c>true</c> get module controls.</param>
        Task<IList<ModuleInfo>> GetModulesListAsync(bool includeModuleControls = false);

        /// <summary>
        /// Gets the modules list by place identifier.
        /// </summary>
        /// <returns>The modules list by place identifier.</returns>
        /// <param name="placeId">Place identifier.</param>
        /// <param name="includeModulePermissions">If set to <c>true</c> get the module permissions.</param>
        Task<IList<ModuleInfo>> GetModulesListByPlaceIdAsync(int placeId, bool includeModulePermissions = false);

        /// <summary>
        /// Gets the modules list by page identifier.
        /// </summary>
        /// <returns>The modules list by page identifier.</returns>
        /// <param name="pageId">Page identifier.</param>
        /// <param name="includeModulePermissions">If set to <c>true</c> get the module permissions.</param>
        Task<IList<ModuleInfo>> GetModulesListByPageIdAsync(int pageId, bool includeModulePermissions = false);

        /// <summary>
        /// Saves the module.
        /// </summary>
        /// <returns><c>true</c>, if module was saved, <c>false</c> otherwise.</returns>
        /// <param name="module">Module.</param>
        Task<bool> SaveModuleAsync(ModuleInfo module);

        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <returns><c>true</c>, if module was deleted, <c>false</c> otherwise.</returns>
        /// <param name="moduleID">Module identifier.</param>
        Task<bool> DeleteModuleAsync(int moduleID);

        #endregion

        #region Module control

        /// <summary>
        /// Gets the module control.
        /// </summary>
        /// <returns>The module control.</returns>
        /// <param name="moduleControlId">Module control identifier.</param>
        Task<ModuleControlInfo> GetModuleControlAsync(int moduleControlId);

        /// <summary>
        /// Gets the module controls list.
        /// </summary>
        /// <returns>The module controls list.</returns>
        /// <param name="moduleDefId">Module def identifier.</param>
        Task<IList<ModuleControlInfo>> GetModuleControlsListAsync(int moduleDefId);

        /// <summary>
        /// Saves the module control.
        /// </summary>
        /// <returns><c>true</c>, if module control was saved, <c>false</c> otherwise.</returns>
        /// <param name="moduleControl">Module control.</param>
        Task<bool> SaveModuleControlAsync(ModuleControlInfo moduleControl);

        /// <summary>
        /// Deletes the module control.
        /// </summary>
        /// <returns><c>true</c>, if module control was deleted, <c>false</c> otherwise.</returns>
        /// <param name="moduleControlId">Module control identifier.</param>
        Task<bool> DeleteModuleControlAsync(int moduleControlId);

        #endregion

        #region Module navigation

        /// <summary>
        /// Gets the module navigation list.
        /// </summary>
        /// <returns>The module controls list.</returns>
        /// <param name="moduleDefinitionId">Module def identifier.</param>
        Task<IList<ModuleNavigationInfo>> GetModuleNavigationListAsync(int moduleDefinitionId);

        /// <summary>
        /// Gets the module navigation list.
        /// </summary>
        /// <returns>The module controls list.</returns>
        /// <param name="type">Module navigation type</param>
        Task<IList<ModuleNavigationInfo>> GetModuleNavigationListByTypeAsync(string type);

        /// <summary>
        /// Saves the module control.
        /// </summary>
        /// <returns><c>true</c>, if module control was saved, <c>false</c> otherwise.</returns>
        /// <param name="moduleNavigation">Module control.</param>
        Task<bool> SaveModuleNavigationAsync(ModuleNavigationInfo moduleNavigation);

        /// <summary>
        /// Deletes the module navigation.
        /// </summary>
        /// <returns><c>true</c>, if module navigation was deleted, <c>false</c> otherwise.</returns>
        /// <param name="moduleNavigationId">Module navigation identifier.</param>
        Task<bool> DeleteModuleNavigationAsync(int moduleNavigationId);

        #endregion
    }
}