using Kastra.Admin.Core.Enums;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface INotificationService
    {
        void CreateToast(string message, ToastEnum type);
    }
}
