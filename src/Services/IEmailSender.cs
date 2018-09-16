using System.Threading.Tasks;

namespace Kastra.Core.Services
{
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email async.
        /// </summary>
        /// <returns>The email async.</returns>
        /// <param name="email">Email.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="message">Message.</param>
        Task SendEmailAsync(string email, string subject, string message);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="message">Message.</param>
        void SendEmail(string email, string subject, string message);
    }
}
