using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkJournal
{
    public class DateModel
    {
        public DateTime AttendanceDate { get; set; }
        public double Grade { get; set; }
        public bool isActive { get; set; }
        public int StudentID { get; set; }
    }

    public class DateModelForPassNoPass
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Grade { get; set; }
        public bool isActive { get; set; }
        public int StudentID { get; set; }
    }

    public class DateViewModelForPassNoPass
    {
        public int SectionID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Grade { get; set; }
        public bool isActive { get; set; }
    }

    public class DateViewModel
    {
        public int SectionID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public double Grade { get; set; }
        public bool isActive { get; set; }
        public string WeekDay { get; set; }
    }

}
