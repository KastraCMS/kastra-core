/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Dto
{
    public class MailTemplateInfo
    {
        /// <summary>
        /// Mail template id.
        /// </summary>
        /// <value></value>
        public int MailtemplateId { get; set; }

        /// <summary>
        /// Mail template keyname.
        /// </summary>
        /// <value></value>
        public string Keyname { get; set; }

        /// <summary>
        /// Mail template value.
        /// </summary>
        /// <value></value>
        public string Value { get; set; }
    }
}