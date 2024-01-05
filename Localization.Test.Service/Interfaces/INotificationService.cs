using Localization.Test.Infrastructure.Models;

namespace Localization.Test.Service.Interfaces
{
    public interface INotificationService
    {
        Task SendRegistrationSuccessfulNotification(User user);
    }
}
