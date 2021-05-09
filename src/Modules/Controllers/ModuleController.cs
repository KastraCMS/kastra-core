/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Linq;
using Kastra.Core.Services.Contracts;
using Kastra.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Kastra.Core.Modules.Controllers
{
    public class ModuleController : Controller
    {
        #region Properties

        public ModuleDefinitionInfo ModuleDefinition
        { 
            get
            {
                if(_moduleDefinition == null)
                {
                    _moduleDefinition = _viewManager.GetModuleDefsList()
                                          .SingleOrDefault(m => m.Namespace == this.GetType().Namespace);
                }

                return _moduleDefinition;
            }
        }

        private ModuleDefinitionInfo _moduleDefinition = null;
        private readonly IViewManager _viewManager = null;

        #endregion

        public ModuleController(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        public override ViewResult View(string viewName)
        {
            if(ModuleDefinition == null)
            {
                return View(viewName);
            }

            return View($"{ModuleDefinition.Path}");
        }
    }
}
