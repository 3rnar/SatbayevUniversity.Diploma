using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.RequestModels
{
    public class AcceptStudentModel
    {
        public int DiplomaID { get; set; }
        public List<StudentIDModel> Students { get; set; }
    }

    public class RejectStudentModel
    {
        public int DiplomaID { get; set; }
        public List<StudentIDModel> Students { get; set; }
        public string Notification { get; set; }
    }
}
