using System;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Kastra.Core.Modules
{
    /// <summary>
    /// An item in <see cref="ModuleViewLocationCacheResult"/>.
    /// </summary>
    internal readonly struct ModuleViewLocationCacheItem
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ModuleViewLocationCacheItem"/>.
        /// </summary>
        /// <param name="razorPageFactory">The <see cref="IRazorPage"/> factory.</param>
        /// <param name="location">The application relative path of the <see cref="IRazorPage"/>.</param>
        public ModuleViewLocationCacheItem(Func<IRazorPage> razorPageFactory, string location)
        {
            PageFactory = razorPageFactory;
            Location = location;
        }

        /// <summary>
        /// Gets the application relative path of the <see cref="IRazorPage"/>
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Gets the <see cref="IRazorPage"/> factory.
        /// </summary>
        public Func<IRazorPage> PageFactory { get; }
    }
}