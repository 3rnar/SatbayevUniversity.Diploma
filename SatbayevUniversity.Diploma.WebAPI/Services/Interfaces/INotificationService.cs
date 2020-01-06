using SatbayevUniversity.Diploma.WebAPI.Models.Notification;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Services
{
    public interface INotificationService
    {
        Task SendNotification(NotificationForDiploma notification);
    }
}