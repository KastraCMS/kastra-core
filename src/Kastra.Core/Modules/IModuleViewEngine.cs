/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Kastra.Core.Modules
{
    public interface IModuleViewEngine : IViewEngine
    {
        ViewEngineResult GetModuleView(string executingFilePath, string moduleViewPath);

        ViewEngineResult FindModuleView(ActionContext viewContext, string qualifiedViewName);
    }
}