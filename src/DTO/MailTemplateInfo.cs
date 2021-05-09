/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.DTO
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
        /// Subject value.
        /// </summary>
        /// <value></value>
        public string Subject { get; set; }

        /// <summary>
        /// Message value.
        /// </summary>
        /// <value></value>
        public string Message { get; set; }
    }
}