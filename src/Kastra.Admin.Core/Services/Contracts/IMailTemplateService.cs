using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IMailTemplateService
    {
        /// <summary>
        /// Load a mail template.
        /// </summary>
        /// <param name="mailTemplateId">Mail template Id</param>
        /// <returns></returns>
        Task<MailTemplateModel> LoadMailTemplate(int mailTemplateId);

        /// <summary>
        /// Load the mail template list.
        /// </summary>
        /// <returns></returns>
        Task<IList<MailTemplateModel>> LoadTemplateList();

        /// <summary>
        /// Save a mail template.
        /// </summary>
        /// <param name="mailtemplate">Mail template</param>
        /// <returns></returns>
        Task<bool> SaveMailtemplate(MailTemplateModel mailtemplate);
    }
}
