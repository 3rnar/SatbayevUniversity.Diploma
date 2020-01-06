using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.RequestModels
{
    public class CreateDiplomaWorkModel
    {
        public string TopicKZ { get; set; }
        public string TopicRU { get; set; }
        public string TopicEN { get; set; }
        public string DescriptionKZ { get; set; }
        public string DescriptionRU { get; set; }
        public string DescriptionEN { get; set; }
        public int TypeID { get; set; }
        public int Amount { get; set; }
        public int GraduationYearID { get; set; }
    }

    public class UpdateDiplomaWorkModel : CreateDiplomaWorkModel
    {
        public int ID { get; set; }
    }
}
