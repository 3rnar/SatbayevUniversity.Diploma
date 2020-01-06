using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkJournal
{
    public class JournalModel
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string StudentFullName { get; set; }
        public string DiplomaWorkTopic { get; set; }
        public int SectionID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public double Grade { get; set; }
        public bool isActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActivePNP { get; set; }
        public string PNP { get; set; }
        public int SectionPNP { get; set; }
    }

    public class JournalViewModel
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string StudentFullName { get; set; }
        public string DiplomaWorkTopic { get; set; }
        public List<DateViewModel> Dates { get; set; }
        public DateViewModelForPassNoPass PNPDate { get; set; }
    }

    public class CreateJournalRequestModel
    {
        public int StudentID { get; set; }
        public int DiplomaWorkID { get; set; }
    }
}

