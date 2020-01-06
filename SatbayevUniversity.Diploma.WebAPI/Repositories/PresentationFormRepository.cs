using Dapper;
using Microsoft.Extensions.Options;
using SatbayevUniversity.Diploma.WebAPI.Models.PresentationForm;
using SatbayevUniversity.Diploma.WebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public class PresentationFormRepository : RepositoryBase<Protocol>, IPresentationFormRepository
    {
        public PresentationFormRepository(IOptionsMonitor<DBConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public void InsertOrUpdateProtocols(List<Protocol> protocols)
        {
            var parameters = new List<DynamicParameters>();

            foreach (var protocol in protocols)
            {
                var queryParameters = new DynamicParameters(protocol);
                parameters.Add(queryParameters);
            }
            using (var db = GetConn())
            {
                db.Execute("Edu_DiplomaProtocols_InsertOrUpdate", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Protocol GetProtocolByStudentID(int studentID)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@StudentID", studentID);
            using (var db = GetConn())
            {
                return db.Query<Protocol>("Edu_DiplomaProtocols_GetByStudentID", queryParameters,
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

        }
    }
}
