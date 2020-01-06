using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.Additions;
using SatbayevUniversity.Diploma.WebAPI.Models.RequestModels;
using SatbayevUniversity.Diploma.WebAPI.Models.ViewModels;
using System.Collections.Generic;

namespace SatbayevUniversity.Diploma.WebAPI.Services
{
    public interface IDiplomaWorkService
    {
        DiplomaWorkModelWithStudentsViewModelAndTotalRowCount GetAllDiplomaWorks(DiplomasFilter filter, string language, string userID);
        DiplomaWorkModelWithoutStudentsViewModelAndTotalRowCount GetAllDiplomaWorksWithoutStudents(DiplomasFilter filter, string language, string userID);
        IEnumerable<InstructorModel> GetAllInstructors();
        IEnumerable<DiplomaWorkTypeModel> GetDiplomaTypes();
        UpdateDiplomaWorkModel GetDiplomaWorkByID(int ID);
        void InsertDiplomaWork(CreateDiplomaWorkModel model, string userID, string language);
        void StudentRequest(List<CreateDiplomaWorkStudentModel> diplomaIDs, string userID, string language);
        void TeacherAcceptStudent(AcceptStudentModel requests);
        void TeacherRejectStudent(RejectStudentModel requests, string body, string userID, string language);
        void UpdateDiplomeWork(int ID, CreateDiplomaWorkModel model);
    }
}