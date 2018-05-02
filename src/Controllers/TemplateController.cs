/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using Kastra.Core.Business;
using Kastra.Core.Dto;
using Kastra.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Kastra.Core.Controllers
{
    public class TemplateController : Controller
    {
        protected readonly IViewManager _viewManager = null;
        protected readonly CacheEngine _cacheEngine = null;
        protected readonly IParameterManager _parameterManager = null;
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

        public IActionResult Index(Int32 pageID, String moduleControl = "")
        {
		    PageInfo page = null;
		    TemplateInfo template = null;
        	IFormCollection form = null;
            ViewEngine viewEngine = null; 
            
			if(pageID <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

		    page = _viewManager.GetPage(pageID, true);
			template = page.PageTemplate;

			// Create template model
			if(!String.IsNullOrEmpty(Request.ContentType))
            {
                form = Request.Form;
            }

            viewEngine = new ViewEngine(page, _cacheEngine);

			object model = viewEngine.CreateView(moduleControl, 0, null);

			return View(template.KeyName, model);
        }

        [Route("[controller]/{pageKeyName}")]
        public IActionResult Index(String pageKeyName)
        {
        	return Index(pageKeyName, 0, String.Empty, String.Empty);
        }

        [Route("[controller]/{pageKeyName}/mid/{moduleId}/mc/{moduleControl}/ma/{moduleAction?}")]
        [Route("[controller]/{pageKeyName}/{moduleId}/{moduleControl}/{moduleAction?}")]
        public IActionResult Index(String pageKeyName, Int32 moduleId, String moduleControl, String moduleAction)
        {
		    PageInfo page = null;
			TemplateInfo template = null;
			IFormCollection form = null;
            ViewEngine viewEngine = null;
            SiteConfigurationInfo siteConfiguration = null;
                  
			if(String.IsNullOrEmpty(pageKeyName))
            {
                return RedirectToAction("Index", "Home");
            }

			page = _viewManager.GetPageByKey(pageKeyName, true);
			template = page.PageTemplate;

            //Get site parameters
            siteConfiguration = _parameterManager.GetSiteConfiguration();

			// Set page title
            ViewBag.Title = String.IsNullOrEmpty(page.Title) ? siteConfiguration.Title : page.Title;

            // Set page SEO
            ViewBag.MetaDescription = String.IsNullOrEmpty(page.MetaDescription) ? siteConfiguration.Description : page.MetaDescription;
            ViewBag.MetaKeywords = page.MetaKeywords;
            ViewBag.MetaRobot = page.MetaRobot;

			if(!String.IsNullOrEmpty(Request.ContentType))
            {
                form = Request.Form;
            }

            viewEngine = new ViewEngine(page, _cacheEngine);

			// Create template model
			object model = viewEngine.CreateView(moduleControl, moduleId, moduleAction);

            // Home page style
            if(pageKeyName.ToLower() == "home")
            {
                ViewBag.IsHome = true;
            }

			return View(template.KeyName, model);
        }

		public IActionResult Home()
		{
			return Index("Home", 0, String.Empty, String.Empty);
		}
    } 
}