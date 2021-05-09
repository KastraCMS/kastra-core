/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;

namespace Kastra.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute
    {
        private string _name = String.Empty;
        public string Name { get { return _name; } }

        public ParameterAttribute(string name)
        {
            _name = name;
        }
    }
}