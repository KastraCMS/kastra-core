/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using Kastra.Core.Dto;

namespace Kastra.Core.Business
{
    public interface IEmailManager
    {
        /// <summary>
        /// Get a mail template with a keyname.
        /// </summary>
        /// <param name="keyname">Mail template keyname</param>
        /// <returns>Mail template</returns>
        TemplateInfo GetMailTemplate(string keyname);

        /// <summary>
        /// Get all mail templates.
        /// </summary>
        /// <returns></returns>
        IList<MailTemplateInfo> GetMailTemplates();

        /// <summary>
        /// Add a mail template.
        /// </summary>
        void AddMailTemplate(string keyname, string value);
        
        /// <summary>
        /// Update a mail template.
        /// </summary>
        void UpdateMailTemplate(MailTemplateInfo mailTemplate);
        
        /// <summary>
        /// Delete a mail template.
        /// </summary>
        void DeleteMailTemplate(string keyname);
    }
}
