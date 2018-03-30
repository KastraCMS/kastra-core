/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace Kastra.Core
{
    public static class KastraRouting
    {
        /// <summary>
        /// Adds the default routes.
        /// </summary>
        /// <param name="routeBuilder">Route builder.</param>
        /// <param name="defaultController">Default controller.</param>
        /// <param name="defaultAdminController">Default admin controller.</param>
        public static void AddDefaultRoutes(this IRouteBuilder routeBuilder, String defaultController, String defaultAdminController)
        {
            if(!String.IsNullOrEmpty(defaultController))
            {
                routeBuilder.MapRoute(
                    name: "default",
                    template: $"{{controller={defaultController}}}/{{action=Home}}/{{id?}}");

                routeBuilder.MapRoute(
                    name: "ModuleRoute",
                    template: $"{defaultController}/{{pa}}/{{mid}}/{{mc}}/{{ma?}}",
                    defaults: new { controller = defaultController, action = "Index" });
            }

            if(!String.IsNullOrEmpty(defaultAdminController))
            {
                routeBuilder.MapRoute(
                    name: "AdminModuleRoute",
                    template: $"{defaultAdminController}/settings/{{mid}}/{{mc}}/{{ma?}}",
                    defaults: new { controller = defaultAdminController, action = "Settings" });
            }
        }
    }
}
