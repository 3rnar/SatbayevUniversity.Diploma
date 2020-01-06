using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.Additions;
using SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkJournal;
using System.Collections.Generic;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public interface IDiplomaWorkJournalRepository
    {
        PaginationModel<JournalModel> GetAll(DiplomasJournalFilter filter, string language);
        PaginationModel<JournalModel> GetAllForPPS(DiplomasJournalFilter filter, string language, int userID);
        void Grading(List<GradingModel> models);
        void GradingPassNoPass(List<PassNoPassModel> models);
        void InsertDiplomaWorkJournal(CreateJournalRequestModel model);
        void InsertDiplomaWorkJournalDates(DateModel model);
        void InsertDiplomaWorkJournalDates(DateModelForPassNoPass model);
    }
}