/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.DTO
{
    public class ModuleNavigationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public int ModuleDefinitionId { get; set; }

        public ModuleDefinitionInfo ModuleDefinition { get; set; } 
    }
}
