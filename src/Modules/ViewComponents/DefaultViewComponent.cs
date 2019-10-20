/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Mvc;

namespace Kastra.Core.Modules.ViewComponents.Default
{
    public class DefaultViewComponent : ViewComponent
    {
        public DefaultViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return Content(string.Empty);
        }
    }
}