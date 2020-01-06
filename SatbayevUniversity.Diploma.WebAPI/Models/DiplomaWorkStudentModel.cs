using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models
{
    public class DiplomaWorkStudentModel
    {
        public int DiplomaID { get; set; }
        public int StudentID { get; set; }
        public int StatusID { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
