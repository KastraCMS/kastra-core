using System;
using System.Collections.Generic;

namespace Kastra.Core.Modules
{
    /// <summary>
    /// Key for entries in RazorViewEngine.ViewLookupCache.
    /// </summary>
    internal readonly struct ModuleViewLocationCacheKey : IEquatable<ModuleViewLocationCacheKey>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ModuleViewLocationCacheKey"/>.
        /// </summary>
        /// <param name="viewName">The view name or path.</param>
        /// <param name="modulePath"></param>
        public ModuleViewLocationCacheKey(
            string viewName,
            string modulePath)
            : this(
                  viewName,
                  modulePath: modulePath,
                  values: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ModuleViewLocationCacheKey"/>.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="values">Values from IViewLocationExpander instances.</param>
        public ModuleViewLocationCacheKey(
            string viewName,
            string modulePath,
            IReadOnlyDictionary<string, string> values)
        {
            ViewName = viewName;
            ModulePath = modulePath;
            ViewLocationExpanderValues = values;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        public string ViewName { get; }

        /// <summary>
        /// Gets the module path.
        /// </summary>
        public string ModulePath { get; }

        /// <summary>
        /// Gets the values populated by IViewLocationExpander instances.
        /// </summary>
        public IReadOnlyDictionary<string, string> ViewLocationExpanderValues { get; }

        /// <inheritdoc />
        public bool Equals(ModuleViewLocationCacheKey y)
        {
            if (!string.Equals(ViewName, y.ViewName, StringComparison.Ordinal))
            {
                return false;
            }

            if (!string.Equals(ModulePath, y.ModulePath, StringComparison.Ordinal))
            {
                return false;
            }

            if (ReferenceEquals(ViewLocationExpanderValues, y.ViewLocationExpanderValues))
            {
                return true;
            }

            if (ViewLocationExpanderValues == null ||
                y.ViewLocationExpanderValues == null ||
                (ViewLocationExpanderValues.Count != y.ViewLocationExpanderValues.Count))
            {
                return false;
            }

            foreach (var item in ViewLocationExpanderValues)
            {
                if (!y.ViewLocationExpanderValues.TryGetValue(item.Key, out var yValue) ||
                    !string.Equals(item.Value, yValue, StringComparison.Ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is ModuleViewLocationCacheKey)
            {
                return Equals((ModuleViewLocationCacheKey)obj);
            }

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCodeCombiner = Microsoft.DotNet.PlatformAbstractions.HashCodeCombiner.Start();
            hashCodeCombiner.Add(ViewName, StringComparer.Ordinal);
            hashCodeCombiner.Add(ModulePath, StringComparer.Ordinal);

            if (ViewLocationExpanderValues != null)
            {
                foreach (var item in ViewLocationExpanderValues)
                {
                    hashCodeCombiner.Add(item.Key, StringComparer.Ordinal);
                    hashCodeCombiner.Add(item.Value, StringComparer.Ordinal);
                }
            }

            return hashCodeCombiner.CombinedHash;
        }
    }
}