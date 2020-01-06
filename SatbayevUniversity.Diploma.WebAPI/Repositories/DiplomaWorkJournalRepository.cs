using Dapper;
using Microsoft.Extensions.Options;
using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.Additions;
using SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkJournal;
using SatbayevUniversity.Diploma.WebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public class DiplomaWorkJournalRepository : RepositoryBase<JournalModel>, IDiplomaWorkJournalRepository
    {
        public DiplomaWorkJournalRepository(IOptionsMonitor<DBConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public PaginationModel<JournalModel> GetAll(DiplomasJournalFilter filter, string language)
        {
            var result = new PaginationModel<JournalModel>();
            var diplomaWorksJournal = new List<JournalModel>();
            using (var db = GetConn())
            {
                var page = filter.PageId - 1;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                queryParameters.Add("@page", page);
                queryParameters.Add("@size", filter.RowCount);
                if (language == "ru")
                {
                    queryParameters.Add("@sort", "TopicRU");
                    diplomaWorksJournal = db.Query<JournalModel>("Edu_GetDiplomaWorksJournalRU_Pagination", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else if (language == "kz")
                {
                    queryParameters.Add("@sort", "TopicKZ");
                    diplomaWorksJournal = db.Query<JournalModel>("Edu_GetDiplomaWorksJournalKZ_Pagination", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else
                {
                    queryParameters.Add("@sort", "TopicEN");
                    diplomaWorksJournal = db.Query<JournalModel>("Edu_GetDiplomaWorksJournalEN_Pagination", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                result.Items = diplomaWorksJournal;

                return result;
            }
        }

        public PaginationModel<JournalModel> GetAllForPPS(DiplomasJournalFilter filter, string language, int userID)
        {
            var result = new PaginationModel<JournalModel>();
            var diplomaWorksJournal = new List<JournalModel>();
            using (var db = GetConn())
            {
                var page = filter.PageId - 1;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("totalrow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                queryParameters.Add("@page", page);
                queryParameters.Add("@size", filter.RowCount);
                queryParameters.Add("@InstructorID", userID);
                queryParameters.Add("@FIO", filter.FIO);
                queryParameters.Add("@IIN", filter.IIN);
                queryParameters.Add("@GraduationYearID", filter.GraduationYearID);
                if (language == "ru")
                {
                    queryParameters.Add("@sort", "TopicRU");
                    diplomaWorksJournal = db.Query<JournalModel>("Edu_GetDiplomaWorksJournalRU_Pagination_ForPPS", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else if (language == "kz")
                {
                    queryParameters.Add("@sort", "TopicKZ");
                    diplomaWorksJournal = db.Query<JournalModel>("Edu_GetDiplomaWorksJournalKZ_Pagination_ForPPS", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                else
                {
                    queryParameters.Add("@sort", "TopicEN");
                    diplomaWorksJournal = db.Query<JournalModel>("Edu_GetDiplomaWorksJournalKZ_Pagination_ForPPS", queryParameters,
                                commandType: CommandType.StoredProcedure).ToList();
                    result.TotalRowCount = queryParameters.Get<int>("totalrow");
                }
                result.Items = diplomaWorksJournal;

                return result;
            }
        }

        public void InsertDiplomaWorkJournal(CreateJournalRequestModel model)
        {
            using (var db = GetConn())
            {
                var queryParameters = new DynamicParameters(model);

                db.Execute("Edu_DiplomaWorkJournal_Insert", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void InsertDiplomaWorkJournalDates(DateModel model)
        {
            using (var db = GetConn())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@AttendanceDate", model.AttendanceDate);
                queryParameters.Add("@Grade", model.Grade);
                queryParameters.Add("@isActive", model.isActive);
                queryParameters.Add("@StudentID", model.StudentID);
                db.Execute("Edu_DiplomaWorkJournalDates_Insert", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void InsertDiplomaWorkJournalDates(DateModelForPassNoPass model)
        {
            using (var db = GetConn())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@StartDate", model.StartDate);
                queryParameters.Add("@EndDate", model.EndDate);
                queryParameters.Add("@Grade", model.Grade);
                queryParameters.Add("@isActive", model.isActive);
                queryParameters.Add("@StudentID", model.StudentID);
                db.Execute("Edu_DiplomaWorkJournalPassNoPassDates_Insert", queryParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Grading(List<GradingModel> models)
        {
            var parameters = new List<DynamicParameters>();

            foreach (var model in models)
            {
                var queryParameters = new DynamicParameters(model);
                parameters.Add(queryParameters);
            }

            using (var db = GetConn())
            {
                db.Execute("Edu_DiplomaWorkJournal_Grading", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void GradingPassNoPass(List<PassNoPassModel> models)
        {
            var parameters = new List<DynamicParameters>();

            foreach (var model in models)
            {
                var queryParameters = new DynamicParameters(model);
                parameters.Add(queryParameters);
            }

            using (var db = GetConn())
            {
                db.Execute("Edu_DiplomaWorkJournal_PassNoPass", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
