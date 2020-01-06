using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public bool HasPhoto { get; set; }
        public string[] Roles { get; set; }
        public double DebtAmount { get; set; }

        /// <summary>
        /// Баурсаков А.А.
        /// </summary>
        /// <returns></returns>
        public string GetShortenedFullName()
        {
            string tok1 = LastName, tok2 = "", tok3 = "";
            if (FirstName != null && FirstName.Length > 0) tok2 = " " + FirstName.ToUpper()[0] + ".";
            if (MiddleName != null && MiddleName.Length > 0) tok3 = " " + MiddleName.ToUpper()[0] + ".";
            return tok1 + tok2 + tok3;
        }
    }
}
