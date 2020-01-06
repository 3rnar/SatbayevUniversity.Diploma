using Dapper;
using Microsoft.Extensions.Options;
using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public class AuthRepository : RepositoryBase<UserInfo>, IAuthRepository
    {
        public AuthRepository(IOptionsMonitor<DBConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public UserInfo GetUserInfo(string userId, string lang)
        {
            using (var db = GetConn())
            {
                return db.QuerySingle<UserInfo>("User_GetUserInfo", new { UserId = userId, Language = lang }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            using (var db = GetConn())
            {
                var res = db.Query<string>("User_Roles_Get", new { UserId = userId }, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }
    }
}
