/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace Kastra.Core.Configuration
{
    public static class KastraRouting
    {
        /// <summary>
        /// Adds the default endpoints.
        /// </summary>
        /// <param name="endpoints">Endpoint route builder.</param>
        /// <param name="defaultController">Default controller.</param>
        /// <param name="defaultAdminController">Default admin controller.</param>
        public static void AddDefaultEndpoints(this IEndpointRouteBuilder endpoints, String defaultController, String defaultAdminController)
        {
            if (!String.IsNullOrEmpty(defaultController))
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: $"{{controller={defaultController}}}/{{action=Home}}/{{id?}}");

                endpoints.MapControllerRoute(
                    name: "ModuleRoute",
                    pattern: $"{defaultController}/{{pa}}/{{mid}}/{{mc}}/{{ma?}}",
                    defaults: new { controller = defaultController, action = "Index" });
            }

            if (!String.IsNullOrEmpty(defaultAdminController))
            {
                endpoints.MapControllerRoute(
                    name: "AdminModuleRoute",
                    pattern: $"{defaultAdminController}/settings/{{mid}}/{{mc}}/{{ma?}}",
                    defaults: new { controller = defaultAdminController, action = "Settings" });
            }
        }
    }
}
