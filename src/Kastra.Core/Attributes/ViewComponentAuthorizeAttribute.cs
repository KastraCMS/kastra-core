/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;

namespace Kastra.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ViewComponentAuthorizeAttribute : Attribute
    {
        public string Claims { get; set; }
        public string Roles { get; set; }

        public string[] GetRoles()
        {
            return DeserializeEntity(Roles);
        }

        public string[] GetClaims()
        {
            return DeserializeEntity(Claims);
        }

        private string[] DeserializeEntity(string entities)
        {
            return entities.Replace(" ", String.Empty).Split(',');
        }
    }
}