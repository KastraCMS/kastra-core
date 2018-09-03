using System;
using System.Linq;
using Kastra.Core.Business;
using Kastra.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Kastra.Core.Controllers
{
    public class ModuleController : Controller
    {
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

        private IViewManager _viewManager = null;


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
