using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.Notification
{
    public class NotificationForDiploma
    {
        public string Body { get; set; }
        public string Header { get; set; }
        public int DayCount { get; set; }
        public int SenderID { get; set; }
        public int RecipientID { get; set; }
    }
}
