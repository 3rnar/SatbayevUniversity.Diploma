using SatbayevUniversity.Diploma.WebAPI.Models;
using System.Collections.Generic;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public interface IAuthRepository
    {
        UserInfo GetUserInfo(string userId, string lang);
        IEnumerable<string> GetUserRoles(string userId);
    }
}