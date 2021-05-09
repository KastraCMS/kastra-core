using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Kastra.Core.Modules.Configuration
{
    /// <summary>
    /// Configuration for <see cref="ModuleViewEngine" />.
    /// </summary>
    public class ModuleViewEngineOptions
    {
        /// <summary>
        /// Gets the locations to find the modules views with the <see cref="ModuleViewEngine" />
        /// </summary>
        public IList<string> ModuleViewLocationFormats { get; } = new List<string>();

        /// <summary>
        /// Gets a <see cref="IList{IViewLocationExpander}"/> used by the <see cref="ModuleRazorViewEngine"/>.
        /// </summary>
        public IList<IViewLocationExpander> ModuleViewLocationExpanders { get; } = new List<IViewLocationExpander>();
    }
}
