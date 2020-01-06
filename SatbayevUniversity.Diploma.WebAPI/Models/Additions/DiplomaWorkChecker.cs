using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.Additions
{
    public class DiplomaWorkChecker
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int Amount { get; set; }
        public int StudentID { get; set; }
        public int StudentStatusID { get; set; }
        public int DiplomaStatusID { get; set; }
    }
}
