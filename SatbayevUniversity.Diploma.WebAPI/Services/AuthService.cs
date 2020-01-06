using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Services.Interfaces
{
    public class AuthService : IAuthService
    {
        IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public UserInfo GetUserInfo(string userId, string lang)
        {
            var key = $"GetUserInfo-{userId}-{lang}";
            //return 
            //HttpContext.Current.Cache.GetItem<UserInfo>(key, DateTime.Now.AddSeconds(30), () =>
            //{
            var userInfo = _repository.GetUserInfo(userId, lang);
            userInfo.Roles = _repository.GetUserRoles(userId).ToArray();

            return userInfo;
            //});
        }
    }
}
