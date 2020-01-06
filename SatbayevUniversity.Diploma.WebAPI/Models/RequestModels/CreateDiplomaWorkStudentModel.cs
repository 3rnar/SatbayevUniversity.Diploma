using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.RequestModels
{
    public class CreateDiplomaWorkStudentModel
    {
        public int DiplomaID { get; set; }
    }

    public class DiplomaIDViewModel
    {
        public int DiplomaID { get; set; }
        public int StudentStatusID { get; set; }
    }
}
