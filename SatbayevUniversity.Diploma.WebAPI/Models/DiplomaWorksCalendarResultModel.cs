using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models
{
    public class DiplomaWorksCalendarResultModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int StudyYear { get; set; }
        public int RegistrationTypeID { get; set; }
        public DateTime StartRegistration { get; set; }
        public DateTime EndRegistration { get; set; }
    }
}
