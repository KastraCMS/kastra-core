using System;

namespace Kastra.Core.Utils.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Throw an argument null exception if the object is null.
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="name">Name</param>
        public static void ThrowIfArgumentNull(this object value, string name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Throw an reference null exception if the object is null.
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="name">Name</param>
        public static void ThrowIfReferenceNull(this object value, string name)
        {
            if (value is null)
            {
                throw new NullReferenceException($"Null reference : {name}");
            }
        }
    }
}
