/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Kastra.Core.Services.Contracts;
using Kastra.Core.Constants;
using Kastra.Core.DTO;
using Kastra.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;

namespace Kastra.Core.Templates.Controllers
{
    public class TemplateController : Controller
    {
        protected readonly IViewManager _viewManager;
        protected readonly CacheEngine _cacheEngine;
        protected readonly IParameterManager _parameterManager;
        protected readonly IViewComponentDescriptorCollectionProvider _viewcomponents;

        public TemplateController(IViewManager viewManager,
                                  CacheEngine cacheEngine,
                                  IViewComponentDescriptorCollectionProvider viewcomponents,
                                  IParameterManager parameterManager)
        {
	 	    _viewManager = viewManager;
            _cacheEngine = cacheEngine;
            _viewcomponents = viewcomponents;
            _parameterManager = parameterManager;
        }

        public async Task<IActionResult> Index(int pageID, string moduleControl = "")
        {
			if(pageID <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            PageInfo page = await _viewManager.GetPageAsync(pageID, true);

            if (page is null)
            {
                return NotFound();
            }

            TemplateInfo template = page.PageTemplate;
            ViewEngine viewEngine = new ViewEngine(page, _cacheEngine);

			object model = viewEngine.CreateView(moduleControl, 0, null);

			return View(template.KeyName, model);
        }

        [Route("[controller]/{pageKeyName}")]
        public Task<IActionResult> Index(string pageKeyName)
        {
        	return Index(pageKeyName, 0, string.Empty, string.Empty);
        }

        [Route("[controller]/{pageKeyName}/{moduleId}/{moduleControl}/{moduleAction?}")]
        [Route("[controller]/{pageKeyName}/mid/{moduleId}/mc/{moduleControl}/ma/{moduleAction?}")]
        public async Task<IActionResult> Index(string pageKeyName, int moduleId, string moduleControl, string moduleAction)
        {
			if(string.IsNullOrEmpty(pageKeyName))
            {
                return RedirectToAction("Index", "Home");
            }

            PageInfo page = await _viewManager.GetPageByKeyAsync(pageKeyName, true);

            if (page is null)
            {
                return NotFound();
            }

            TemplateInfo template = page.PageTemplate;

            //Get site parameters
            SiteConfigurationInfo siteConfiguration = await _parameterManager.GetSiteConfigurationAsync();

			// Set page title
            ViewBag.Title = string.IsNullOrEmpty(page.Title) ? siteConfiguration.Title : page.Title;

            // Set theme
            ViewBag.Theme = siteConfiguration.Theme ?? SiteConfiguration.DefaultTheme;

            // Set page SEO
            ViewBag.MetaDescription = string.IsNullOrEmpty(page.MetaDescription) ? siteConfiguration.Description : page.MetaDescription;
            ViewBag.MetaKeywords = page.MetaKeywords;
            ViewBag.MetaRobot = page.MetaRobot;

            ViewEngine viewEngine = new ViewEngine(page, _cacheEngine);

			// Create template model
			object model = viewEngine.CreateView(moduleControl, moduleId, moduleAction);

            // Home page style
            if(pageKeyName.ToLower() == "home")
            {
                ViewBag.IsHome = true;
            }

			return View(template.KeyName, model);
        }
    } 
}