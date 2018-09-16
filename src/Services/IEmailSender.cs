using System.Threading.Tasks;

namespace Kastra.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        void SendEmail(string email, string subject, string message);
    }
}
