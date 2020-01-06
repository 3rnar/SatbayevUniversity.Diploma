using Newtonsoft.Json;
using SatbayevUniversity.Diploma.WebAPI.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Services
{
    public class NotificationService : INotificationService
    {
        public async Task SendNotification(NotificationForDiploma notification)
        {
            var json = JsonConvert.SerializeObject(notification);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = "http://api.devkaznitu.kz/api/Notification/SendNotification";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
        }
    }
}
