using SatbayevUniversity.Diploma.WebAPI.Models;

namespace SatbayevUniversity.Diploma.WebAPI.Services.Interfaces
{
    public interface IAuthService
    {
        UserInfo GetUserInfo(string userId, string lang);
    }
}