/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using static Kastra.Core.Constants;

namespace Kastra.Core.Modules
{
    public class ModuleViewComponentResult : IViewComponentResult
    {
        // {0} is the module name, {1} is the component view name.
        private const string ModulePathFormat = "{0}/Views/{1}";

        /// <summary>
        /// Gets or sets the module definition path;
        /// </summary>
        public string ModuleDefinitionPath { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ViewDataDictionary"/>.
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ITempDataDictionary"/> instance.
        /// </summary>
        public ITempDataDictionary TempData { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ViewEngine"/>.
        /// </summary>
        public IModuleViewEngine ViewEngine { get; set; }

        /// <summary>
        /// Locates and renders a view specified by <see cref="ViewName"/>. If <see cref="ViewName"/> is <c>null</c>,
        /// then the view name searched for is<c>&quot;Default&quot;</c>.
        /// </summary>
        /// <param name="context">The <see cref="ViewComponentContext"/> for the current component execution.</param>
        /// <remarks>
        /// This method synchronously calls and blocks on <see cref="ExecuteAsync(ViewComponentContext)"/>.
        /// </remarks>
        public void Execute(ViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var task = ExecuteAsync(context);
            task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Locates and renders a module view specified by <see cref="ViewName"/>. If <see cref="ViewName"/> is <c>null</c>,
        /// then the view name searched for is<c>&quot;Default&quot;</c>.
        /// </summary>
        /// <param name="context">The <see cref="ViewComponentContext"/> for the current component execution.</param>
        /// <returns>A <see cref="Task"/> which will complete when view rendering is completed.</returns>
        public async Task ExecuteAsync(ViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (ModuleDefinitionPath == null)
            {
                throw new ArgumentNullException(nameof(ModuleDefinitionPath));
            }

            IModuleViewEngine moduleViewEngine = ViewEngine ?? ResolveModuleViewEngine(context);
            ViewContext viewContext = context.ViewContext;
            
            bool isNullOrEmptyViewName = string.IsNullOrEmpty(ViewName);
            IEnumerable<string> originalLocations = null;

            string viewName = isNullOrEmptyViewName ? SiteConfig.DefaultModuleViewName : ViewName;

            string qualifiedViewName = string.Format(
                CultureInfo.InvariantCulture,
                ModulePathFormat,
                ModuleDefinitionPath,
                viewName);

            ViewEngineResult result = moduleViewEngine.FindModuleView(viewContext, qualifiedViewName);
            IView view = result.EnsureSuccessful(originalLocations).View;

            using (view as IDisposable)
            {
                var childViewContext = new ViewContext(
                    viewContext,
                    view,
                    ViewData ?? context.ViewData,
                    context.Writer);

                await view.RenderAsync(childViewContext);
            }
        }

        private static IModuleViewEngine ResolveModuleViewEngine(ViewComponentContext context)
        {
            var viewEngines = context.ViewContext.HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>().ViewEngines;

            for (int i = 0; i < viewEngines.Count; i++)
            {
                if (viewEngines[i] is IModuleViewEngine)
                {
                    return viewEngines[i] as IModuleViewEngine;
                }
            }

            return null;
        }

        public static string NormalizePath(string path)
        {
            var addLeadingSlash = path[0] != '\\' && path[0] != '/';
            var transformSlashes = path.IndexOf('\\') != -1;

            if (!addLeadingSlash && !transformSlashes)
            {
                return path;
            }

            var length = path.Length;
            if (addLeadingSlash)
            {
                length++;
            }

            return string.Create(length, (path, addLeadingSlash), (span, tuple) =>
            {
                var (pathValue, addLeadingSlashValue) = tuple;
                var spanIndex = 0;

                if (addLeadingSlashValue)
                {
                    span[spanIndex++] = '/';
                }

                foreach (var ch in pathValue)
                {
                    span[spanIndex++] = ch == '\\' ? '/' : ch;
                }
            });
        }
    }
}
