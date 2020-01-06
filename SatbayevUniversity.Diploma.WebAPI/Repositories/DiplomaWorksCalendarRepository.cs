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
    public class DiplomaWorksCalendarRepository : RepositoryBase<DiplomaWorksCalendarResultModel>, IDiplomaWorksCalendarRepository
    {
        public DiplomaWorksCalendarRepository(IOptionsMonitor<DBConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public void Delete(int id)
        {
            using (var db = GetConn())
            {
                db.Execute("Edu_DiplomaWorksCalendar_Delete", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<DiplomaWorksCalendarResultModel> GetAll(DiplomaWorksCalendarFilterModel filter)
        {
            using (var db = GetConn())
            {
                return db.Query<DiplomaWorksCalendarResultModel>("Edu_DiplomaWorksCalendar_Get", filter, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void InsertOrUpdate(string userId, DiplomaWorksCalendarResultModel model)
        {
            using (var db = GetConn())
            {
                db.Execute("Edu_DiplomaWorksCalendar_InsertOrUpdate", new
                {
                    model.ID,
                    model.StudyYear,
                    model.RegistrationTypeID,
                    model.StartRegistration,
                    model.EndRegistration
                }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
