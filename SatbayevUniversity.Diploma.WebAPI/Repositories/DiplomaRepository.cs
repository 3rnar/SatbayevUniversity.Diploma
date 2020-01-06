using Dapper;
using Microsoft.Extensions.Options;
using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.Additions;
using SatbayevUniversity.Diploma.WebAPI.Models.RequestModels;
using SatbayevUniversity.Diploma.WebAPI.Models.ViewModels;
using SatbayevUniversity.Diploma.WebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public class DiplomaRepository : RepositoryBase<DiplomaWorkModel>, IDiplomaRepository
    {
        public DiplomaRepository(IOptionsMonitor<DBConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public UpdateDiplomaWorkModel GetDiplomaWorkByID(int ID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ID", ID);
                connection.Open();
                return connection.Query<UpdateDiplomaWorkModel>("Edu_GetDiplomaWorkByID", queryParameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public DiplomaWorkViewModelWithTotalRowCount GetAllDiplomaWorks(DiplomasFilter filter, string language, int userID)
        {
            var result = new DiplomaWorkViewModelWithTotalRowCount();
            var diplomaWorks = new List<DiplomaWorkViewModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                var page = filter.PageId - 1;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                queryParameters.Add("@page", page);
                queryParameters.Add("@size", filter.RowCount);
                queryParameters.Add("@GraduationYearID", filter.GraduationYearID);
                queryParameters.Add("@InstructorName", filter.InstructorName);
                queryParameters.Add("@InstructorID", userID);
                if (language == "ru")
                {
                    connection.Open();
                    queryParameters.Add("@sort", "TopicRU");
                    diplomaWorks = connection.Query<DiplomaWorkViewModel>("Edu_GetDiplomaWorksRU_Pagination", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else if (language == "kz")
                {
                    connection.Open();
                    queryParameters.Add("@sort", "TopicKZ");
                    diplomaWorks = connection.Query<DiplomaWorkViewModel>("Edu_GetDiplomaWorksKZ_Pagination", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else
                {
                    connection.Open();
                    queryParameters.Add("@sort", "TopicEN");
                    diplomaWorks = connection.Query<DiplomaWorkViewModel>("Edu_GetDiplomaWorksEN_Pagination", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                result.DiplomaWorks = diplomaWorks;

                return result;
            }
        }


        public DiplomaWorkViewModelWithoutStudents_WithTotalRowCount GetAllDiplomaWorksWithoutStudents(DiplomasFilter filter, string language)
        {
            var result = new DiplomaWorkViewModelWithoutStudents_WithTotalRowCount();
            var diplomaWorks = new List<DiplomaWorkViewModelWithoutStudents>();
            using (var connection = new SqlConnection(connectionString))
            {
                var page = filter.PageId - 1;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                queryParameters.Add("@page", page);
                queryParameters.Add("@size", filter.RowCount);
                queryParameters.Add("@InstructorName", filter.InstructorName);

                connection.Open();
                if (language == "ru")
                {
                    queryParameters.Add("@sort", "TopicRU");
                    diplomaWorks = connection.Query<DiplomaWorkViewModelWithoutStudents>("Edu_GetDiplomaWorksRU_Pagination_WithoutStudents", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else if (language == "kz")
                {
                    queryParameters.Add("@sort", "TopicKZ");
                    diplomaWorks = connection.Query<DiplomaWorkViewModelWithoutStudents>("Edu_GetDiplomaWorksKZ_Pagination_WithoutStudents", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else
                {
                    queryParameters.Add("@sort", "TopicEN");
                    diplomaWorks = connection.Query<DiplomaWorkViewModelWithoutStudents>("Edu_GetDiplomaWorksEN_Pagination_WithoutStudents", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                result.DiplomaWorks = diplomaWorks;

                return result;
            }
        }

        public IEnumerable<DiplomaWorkChecker> CheckStudentsCount(int diplomaID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ID", diplomaID);
                connection.Open();
                return connection.Query<DiplomaWorkChecker>("Edu_CheckStudentsCount", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateDiplomaWork(int ID, UpdateDiplomaWorkViewModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var queryParameters = new DynamicParameters(model);
                queryParameters.Add("@ID", ID);
                connection.Execute("Edu_DiplomaWork_Update", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void InsertDiplomaWork(DiplomaWorkModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var queryParameters = new DynamicParameters(model);

                connection.Open();
                connection.Execute("Edu_DiplomaWork_Insert", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<DiplomaWorkTypeModel> GetDiplomaTypes()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<DiplomaWorkTypeModel>("Edu_GetDiplomaTypes",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<DictionaryModel> GetGraduationYears()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<DictionaryModel>("Edu_GetGraduationYears",
                    commandType: CommandType.StoredProcedure);
            }
        }


        public void StudentRequest(IEnumerable<DiplomaWorkStudentModel> students)
        {
            var parameters = new List<DynamicParameters>();

            foreach (var student in students)
            {
                var queryParameters = new DynamicParameters(student);
                parameters.Add(queryParameters);
            }
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("Edu_DiplomaWork_Students_Insert", parameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public bool IsHaveAcceptedDiplomaWork(int studentID)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@StudentID", studentID);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<bool>("Edu_IsHaveAcceptedDiplomaWork", queryParameters,
                commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public bool IsHaveSimilarDiplomaWork(int studentID, int diplomaID)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@StudentID", studentID);
            queryParameters.Add("@DiplomaID", diplomaID);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<bool>("Edu_IsHaveSimilarDiplomaWork", queryParameters,
                commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public IEnumerable<DiplomaWorkStudentModelWithID> GetStudentsByDiplomaID(int diplomaID)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@DiplomaID", diplomaID);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<DiplomaWorkStudentModelWithID>("Edu_GetDiplomaWorkStudents", queryParameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<DiplomaWorkStudentModelWithID> GetStudentsByStudentID(int studentID)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@StudentID", studentID);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<DiplomaWorkStudentModelWithID>("Edu_GetDiplomaWorkStudentsByStudentID", queryParameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<DiplomaIDViewModel> GetAllDiplomaWorksByStudentID(int studentID)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@StudentID", studentID);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<DiplomaIDViewModel>("Edu_GetAllDiplomaWorkIDsByStudentID", queryParameters,
                commandType: CommandType.StoredProcedure);
            }
        }
        public void UpdateManyStudents(List<DiplomaWorkStudentModelWithID> students)
        {
            var parameters = new List<DynamicParameters>();

            foreach (var student in students)
            {
                var queryParameters = new DynamicParameters(student);
                parameters.Add(queryParameters);
            }
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("Edu_UpdateDiplomaWorkStudents", parameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateStudents(DiplomaWorkStudentModelWithID student)
        {
            var queryParameters = new DynamicParameters(student);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("Edu_UpdateDiplomaWorkStudents", queryParameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public void ChangeDiplomaWorkStatus(int diplomaID, int statusID, DateTime updatedTime)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ID", diplomaID);
            queryParameters.Add("@StatusID", statusID);
            queryParameters.Add("@UpdatedTime", updatedTime);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("Edu_ChangeDiplomaWorkStatus", queryParameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<InstructorModel> GetAllInstructors()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<InstructorModel>("Edu_GetDiplomaWorkInstructors",
                commandType: CommandType.StoredProcedure);
            }
        }
    }
}
