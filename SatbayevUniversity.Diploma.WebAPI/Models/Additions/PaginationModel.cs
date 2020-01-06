using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.Additions
{
    public class PaginationModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalRowCount { get; set; }
    }
}
