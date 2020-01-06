using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models
{
    public class DiplomasFilter
    {
        public int? PageId { get; set; }
        public int? RowCount { get; set; }
        public int GraduationYearID { get; set; }
        public string InstructorName { get; set; }
    }

    public class DiplomasJournalFilter
    {
        public int? PageId { get; set; }
        public int? RowCount { get; set; }
        public int GraduationYearID { get; set; }
        public string FIO { get; set; }
        public string IIN { get; set; }
    }
}
