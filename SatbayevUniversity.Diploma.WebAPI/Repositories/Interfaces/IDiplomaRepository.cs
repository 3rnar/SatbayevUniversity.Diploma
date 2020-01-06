using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.Additions;
using SatbayevUniversity.Diploma.WebAPI.Models.RequestModels;
using SatbayevUniversity.Diploma.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public interface IDiplomaRepository
    {
        void ChangeDiplomaWorkStatus(int diplomaID, int statusID, DateTime updatedTime);
        IEnumerable<DiplomaWorkChecker> CheckStudentsCount(int diplomaID);
        DiplomaWorkViewModelWithTotalRowCount GetAllDiplomaWorks(DiplomasFilter filter, string language, int userID);
        IEnumerable<DiplomaIDViewModel> GetAllDiplomaWorksByStudentID(int studentID);
        DiplomaWorkViewModelWithoutStudents_WithTotalRowCount GetAllDiplomaWorksWithoutStudents(DiplomasFilter filter, string language);
        IEnumerable<InstructorModel> GetAllInstructors();
        IEnumerable<DiplomaWorkTypeModel> GetDiplomaTypes();
        UpdateDiplomaWorkModel GetDiplomaWorkByID(int ID);
        IEnumerable<DictionaryModel> GetGraduationYears();
        IEnumerable<DiplomaWorkStudentModelWithID> GetStudentsByDiplomaID(int diplomaID);
        IEnumerable<DiplomaWorkStudentModelWithID> GetStudentsByStudentID(int studentID);
        void InsertDiplomaWork(DiplomaWorkModel model);
        bool IsHaveAcceptedDiplomaWork(int studentID);
        bool IsHaveSimilarDiplomaWork(int studentID, int diplomaID);
        void StudentRequest(IEnumerable<DiplomaWorkStudentModel> students);
        void UpdateDiplomaWork(int ID, UpdateDiplomaWorkViewModel model);
        void UpdateManyStudents(List<DiplomaWorkStudentModelWithID> students);
        void UpdateStudents(DiplomaWorkStudentModelWithID student);
    }
}