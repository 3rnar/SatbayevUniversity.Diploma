using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.PresentationForm
{
    public class Protocol
    {
        public int StudentID { get; set; }
        public DateTime DateOfProtocol { get; set; }
        public string NumberOfProtocol { get; set; }
    }
}
