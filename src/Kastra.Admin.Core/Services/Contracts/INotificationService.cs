using Kastra.Admin.Core.Components;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface INotificationService
    {
        void CreateToast(string message, ToastEnum type);
    }
}
