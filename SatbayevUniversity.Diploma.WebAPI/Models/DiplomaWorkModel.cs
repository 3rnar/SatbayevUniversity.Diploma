using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models
{
    public class DiplomaWorkModel : UpdateDiplomaWorkViewModel
    {
        public DateTime AddedTime { get; set; }
    }
    public class UpdateDiplomaWorkViewModel
    {
        public string TopicKZ { get; set; }
        public string TopicRU { get; set; }
        public string TopicEN { get; set; }
        public string DescriptionKZ { get; set; }
        public string DescriptionRU { get; set; }
        public string DescriptionEN { get; set; }
        public int TypeID { get; set; }
        public int Amount { get; set; }
        public int InstructorID { get; set; }
        public int GraduationYearID { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int StatusID { get; set; }
    }
}
