/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Kastra.Core.DTO;

namespace Kastra.Core.Services.Contracts
{
    public interface IEmailManager
    {
        /// <summary>
        /// Get a mail template with a keyname.
        /// </summary>
        /// <param name="keyname">Mail template keyname</param>
        /// <returns>Mail template</returns>
        Task<MailTemplateInfo> GetMailTemplateAsync(string keyname);

        /// <summary>
        /// Get all mail templates.
        /// </summary>
        /// <returns></returns>
        Task<IList<MailTemplateInfo>> GetMailTemplatesAsync();

        /// <summary>
        /// Add a mail template.
        /// </summary>
        Task AddMailTemplateAsync(string keyname, string subject, string message);
        
        /// <summary>
        /// Update a mail template.
        /// </summary>
        Task UpdateMailTemplateAsync(MailTemplateInfo mailTemplate);
        
        /// <summary>
        /// Delete a mail template.
        /// </summary>
        Task DeleteMailTemplateAsync(string keyname);

        /// <summary>
        /// Replace all keys with values in the template. 
        /// </summary>
        /// <param name="template">Template</param>
        /// <param name="data">The keys with the values to replace in the template.</param>
        /// <returns></returns>
        string Format(string template, Dictionary<string, string> data);

        /// <summary>
        /// Send an mail by using a mail template.
        /// </summary>
        /// <param name="email">Receiver email.</param>
        /// <param name="templateName">Template name.</param>
        /// <param name="data">Data to replace in the mail body or title.</param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string templateName, Dictionary<string, string> data);

        /// <summary>
        /// Send an mail by using a mail template.
        /// </summary>
        /// <param name="email">Receiver email.</param>
        /// <param name="templateName">Template name.</param>
        /// <param name="data">Data to replace in the mail body or title.</param>
        void SendEmail(string email, string templateName, Dictionary<string, string> data);
    }
}
