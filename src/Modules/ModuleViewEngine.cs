/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using Kastra.Core.Modules.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Kastra.Core.Modules
{
    public class ModuleViewEngine : RazorViewEngine, IModuleViewEngine
    {
        public const string ModuleKey = "module";
        public static readonly string ModuleViewExtension = ".cshtml";

        private static readonly TimeSpan _cacheExpirationDuration = TimeSpan.FromMinutes(20);

        private readonly IRazorPageFactoryProvider _pageFactory;
        private readonly IRazorPageActivator _pageActivator;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ModuleViewEngineOptions _options;
        private readonly DiagnosticListener _diagnosticListener;

        public ModuleViewEngine(IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            IOptions<ModuleViewEngineOptions> moduleOptionsAccessor,
            ILoggerFactory loggerFactory,
            DiagnosticListener diagnosticListener)
            : base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, loggerFactory, diagnosticListener)
        {
            _options = moduleOptionsAccessor.Value;
            _pageFactory = pageFactory;
            _pageActivator = pageActivator;
            _htmlEncoder = htmlEncoder;
            _diagnosticListener = diagnosticListener;
        }

        public ViewEngineResult FindModuleView(ActionContext context, string viewName)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException(Resources.ArgumentCannotBeNullOrEmpty, nameof(viewName));
            }

            if (IsApplicationRelativePath(viewName) || IsRelativePath(viewName))
            {
                // A path; not a name this method can handle.
                return ViewEngineResult.NotFound(viewName, Enumerable.Empty<string>());
            }

            var cacheResult = LocatePageFromViewLocations(context, viewName);

            return CreateViewEngineResult(cacheResult, viewName);
        }

        public ViewEngineResult GetModuleView(string executingFilePath, string viewPath)
        {
            if (string.IsNullOrEmpty(viewPath))
            {
                throw new ArgumentException(Resources.ArgumentCannotBeNullOrEmpty, nameof(viewPath));
            }

            if (!(IsApplicationRelativePath(viewPath) || IsRelativePath(viewPath)))
            {
                // Not a path this method can handle.
                return ViewEngineResult.NotFound(viewPath, Enumerable.Empty<string>());
            }

            var cacheResult = LocatePageFromPath(executingFilePath, viewPath);
            return CreateViewEngineResult(cacheResult, viewPath);
        }


        private ModuleViewLocationCacheResult LocatePageFromPath(string executingFilePath, string pagePath)
        {
            var applicationRelativePath = GetAbsolutePath(executingFilePath, pagePath);
            var cacheKey = new ModuleViewLocationCacheKey(applicationRelativePath, null);

            if (!ViewLookupCache.TryGetValue(cacheKey, out ModuleViewLocationCacheResult cacheResult))
            {
                var expirationTokens = new HashSet<IChangeToken>();
                cacheResult = CreateCacheResult(expirationTokens, applicationRelativePath);

                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.SetSlidingExpiration(_cacheExpirationDuration);
                foreach (var expirationToken in expirationTokens)
                {
                    cacheEntryOptions.AddExpirationToken(expirationToken);
                }

                // No views were found at the specified location. Create a not found result.
                if (cacheResult == null)
                {
                    cacheResult = new ModuleViewLocationCacheResult(new[] { applicationRelativePath });
                }

                cacheResult = ViewLookupCache.Set(
                    cacheKey,
                    cacheResult,
                    cacheEntryOptions);
            }

            return cacheResult;
        }

        private ModuleViewLocationCacheResult LocatePageFromViewLocations(
            ActionContext actionContext,
            string moduleViewName)
        {
            var expanderContext = new ViewLocationExpanderContext(
                actionContext,
                moduleViewName,
                null,
                null,
                null,
                false);

            Dictionary<string, string> expanderValues = null;

            var expanders = _options.ModuleViewLocationExpanders;
            var expandersCount = expanders.Count;

            if (expandersCount > 0)
            {
                expanderValues = new Dictionary<string, string>(StringComparer.Ordinal);
                expanderContext.Values = expanderValues;

                for (var i = 0; i < expandersCount; i++)
                {
                    expanders[i].PopulateValues(expanderContext);
                }
            }

            var cacheKey = new ModuleViewLocationCacheKey(
                expanderContext.ViewName,
                expanderValues);

            if (!ViewLookupCache.TryGetValue(cacheKey, out ModuleViewLocationCacheResult cacheResult))
            {
                cacheResult = OnCacheMiss(expanderContext, cacheKey);
            }

            return cacheResult;
        }

        private ViewEngineResult CreateViewEngineResult(ModuleViewLocationCacheResult result, string viewName)
        {
            if (!result.Success)
            {
                return ViewEngineResult.NotFound(viewName, result.SearchedLocations);
            }

            var page = result.ViewEntry.PageFactory();

            var viewStarts = new IRazorPage[result.ViewStartEntries.Count];
            for (var i = 0; i < viewStarts.Length; i++)
            {
                var viewStartItem = result.ViewStartEntries[i];
                viewStarts[i] = viewStartItem.PageFactory();
            }

            var view = new RazorView(this, _pageActivator, viewStarts, page, _htmlEncoder, _diagnosticListener);
            return ViewEngineResult.Found(viewName, view);
        }

        private ModuleViewLocationCacheResult CreateCacheResult(
            HashSet<IChangeToken> expirationTokens,
            string relativePath)
        {
            var factoryResult = _pageFactory.CreateFactory(relativePath);
            var viewDescriptor = factoryResult.ViewDescriptor;

            if (viewDescriptor?.ExpirationTokens != null)
            {
                var viewExpirationTokens = viewDescriptor.ExpirationTokens;
                var viewExpirationTokensCount = viewExpirationTokens.Count;

                for (var i = 0; i < viewExpirationTokensCount; i++)
                {
                    expirationTokens.Add(viewExpirationTokens[i]);
                }
            }

            if (factoryResult.Success)
            {
                return new ModuleViewLocationCacheResult(
                    new ModuleViewLocationCacheItem(factoryResult.RazorPageFactory, relativePath),
                    Array.Empty<ModuleViewLocationCacheItem>());
            }

            return null;
        }

        private ModuleViewLocationCacheResult OnCacheMiss(
            ViewLocationExpanderContext expanderContext,
            ModuleViewLocationCacheKey cacheKey)
        {
            IList<string> viewLocations = _options.ModuleViewLocationFormats;


            ModuleViewLocationCacheResult cacheResult = null;
            List<string> searchedLocations = new List<string>();
            HashSet<IChangeToken> expirationTokens = new HashSet<IChangeToken>();

            foreach (var location in viewLocations)
            {
                var path = string.Format(
                    CultureInfo.InvariantCulture,
                    location,
                    expanderContext.ViewName, null);

                path = ModuleViewEnginePath.ResolvePath(path);

                cacheResult = CreateCacheResult(expirationTokens, path);
                if (cacheResult != null)
                {
                    break;
                }

                searchedLocations.Add(path);
            }

            // No views were found at the specified location. Create a not found result.
            if (cacheResult == null)
            {
                cacheResult = new ModuleViewLocationCacheResult(searchedLocations);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(_cacheExpirationDuration);
            foreach (var expirationToken in expirationTokens)
            {
                cacheEntryOptions.AddExpirationToken(expirationToken);
            }

            return ViewLookupCache.Set(cacheKey, cacheResult, cacheEntryOptions);
        }

        private static bool IsApplicationRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            return name[0] == '~' || name[0] == '/';
        }

        private static bool IsRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            // Though ./ViewName looks like a relative path, framework searches for that view using view locations.
            return name.EndsWith(ModuleViewExtension, StringComparison.OrdinalIgnoreCase);
        }
    }
}
