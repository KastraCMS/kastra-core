using System;
using Microsoft.AspNetCore.Mvc;

namespace Kastra.Core.Default
{
    public class DefaultViewComponent : ViewComponent
    {
        public DefaultViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return Content(String.Empty);
        }
    }
}