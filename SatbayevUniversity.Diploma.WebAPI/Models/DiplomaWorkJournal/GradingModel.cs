using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkJournal
{
    public class GradingModel
    {
        public int SectionID { get; set; }
        public double Grade { get; set; }
    }
    public class PassNoPassModel
    {
        public int SectionID { get; set; }
        public string Grade { get; set; }
    }
}
