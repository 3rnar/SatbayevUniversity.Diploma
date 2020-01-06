using SatbayevUniversity.Diploma.WebAPI.Models;
using System.Collections.Generic;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public interface IDiplomaWorksCalendarRepository
    {
        void Delete(int id);
        IEnumerable<DiplomaWorksCalendarResultModel> GetAll(DiplomaWorksCalendarFilterModel filter);
        void InsertOrUpdate(string userId, DiplomaWorksCalendarResultModel model);
    }
}