using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.Additions;
using SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkEnums;
using SatbayevUniversity.Diploma.WebAPI.Models.DiplomaWorkJournal;
using SatbayevUniversity.Diploma.WebAPI.Models.Notification;
using SatbayevUniversity.Diploma.WebAPI.Models.RequestModels;
using SatbayevUniversity.Diploma.WebAPI.Models.ViewModels;
using SatbayevUniversity.Diploma.WebAPI.Repositories;
using SatbayevUniversity.Diploma.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Services
{
    public class DiplomaWorkService : IDiplomaWorkService
    {
        IDiplomaRepository _diplomaRepository;
        IAuthService _authService;
        IDiplomaWorksCalendarRepository _calendarRepository;
        IDiplomaWorkJournalRepository _journalRepository;
        INotificationService _notificationService;

        public DiplomaWorkService(IDiplomaRepository diplomaRepository,
                                  IAuthService authService,
                                  IDiplomaWorksCalendarRepository calendarRepository,
                                  IDiplomaWorkJournalRepository journalRepository,
                                  INotificationService notificationService)
        {
            _diplomaRepository = diplomaRepository;
            _authService = authService;
            _calendarRepository = calendarRepository;
            _journalRepository = journalRepository;
            _notificationService = notificationService;
        }

        public DiplomaWorkModelWithStudentsViewModelAndTotalRowCount GetAllDiplomaWorks(DiplomasFilter filter, string language, string userID)
        {
            if (filter.GraduationYearID == 0)
            {
                filter.GraduationYearID = DateTime.Now.Year;
            }
            int _userID = 34780;
            if (userID != null)
            {
                _userID = _authService.GetUserInfo(userID, language).Id;
            }

            var diplomaTopics = _diplomaRepository.GetAllDiplomaWorks(filter, language, _userID);

            var diplomaWorkIDs = diplomaTopics.DiplomaWorks.Select(topics => topics.ID).Distinct();

            var result = new DiplomaWorkModelWithStudentsViewModelAndTotalRowCount();
            result.DiplomaWorks = new List<DiplomaWorkWithStudentsViewModel>();
            result.TotalRowCount = diplomaTopics.TotalRowCount;
            foreach (var diplomaWorkID in diplomaWorkIDs)
            {
                var topic = diplomaTopics.DiplomaWorks.FirstOrDefault(x => x.ID == diplomaWorkID);
                var resultTopic = new DiplomaWorkWithStudentsViewModel()
                {
                    ID = topic.ID,
                    FullName = topic.FullName,
                    Topic = topic.Topic,
                    Description = topic.Description,
                    Type = topic.Type,
                    Amount = topic.Amount,
                    GraduationYearID = topic.GraduationYearID,
                    InstructorID = topic.InstructorID,
                    StatusID = topic.StatusID,
                    DiplomaStatus = topic.DiplomaStatus,
                    SubmittedStudents = topic.StudentID == 0 ? null : diplomaTopics.DiplomaWorks.Where(z => z.ID == diplomaWorkID && (DiplomaWorkStudentStatus)z.StudentStatusID == DiplomaWorkStudentStatus.Accepted).Select(y => new DiplomaWorksStudentsViewModel()
                    {
                        FIO = y.ShortName,
                        Status = y.Status,
                        StudentID = y.StudentID
                    }).ToList(),
                    WaitingStudents = topic.StudentID == 0 ? null : diplomaTopics.DiplomaWorks.Where(z => z.ID == diplomaWorkID && (DiplomaWorkStudentStatus)z.StudentStatusID == DiplomaWorkStudentStatus.New).Select(y => new DiplomaWorksStudentsViewModel()
                    {
                        FIO = y.ShortName,
                        Status = y.Status,
                        StudentID = y.StudentID
                    }).ToList()
                };
                result.DiplomaWorks.Add(resultTopic);
            }
            return result;
        }

        public UpdateDiplomaWorkModel GetDiplomaWorkByID(int ID)
        {
            return _diplomaRepository.GetDiplomaWorkByID(ID);
        }

        public void UpdateDiplomeWork(int ID, CreateDiplomaWorkModel model)
        {

            if (model.TypeID == 0 || model.Amount == 0 || model.GraduationYearID == 0 || model.TopicKZ == null || model.TopicRU == null || model.TopicEN == null || model.DescriptionEN == null || model.DescriptionKZ == null || model.DescriptionRU == null)
            {
                throw new Exception("Все поля должны быть заполнены!");
            }

            var newDiplomaWork = new UpdateDiplomaWorkViewModel()
            {
                TopicKZ = model.TopicKZ,
                TopicRU = model.TopicRU,
                TopicEN = model.TopicEN,
                DescriptionKZ = model.DescriptionKZ,
                DescriptionRU = model.DescriptionRU,
                DescriptionEN = model.DescriptionEN,
                GraduationYearID = model.GraduationYearID,
                UpdatedTime = DateTime.Now,
                Amount = (DiplomaWorkType)model.TypeID == DiplomaWorkType.Individual ? 1 : model.Amount,
                TypeID = model.TypeID,
            };

            _diplomaRepository.UpdateDiplomaWork(ID, newDiplomaWork);
        }


        public void InsertDiplomaWork(CreateDiplomaWorkModel model, string userID, string language)
        {

            if (model.TypeID == 0 || model.Amount == 0 || model.GraduationYearID == 0 || model.TopicKZ == null || model.TopicRU == null || model.TopicEN == null || model.DescriptionEN == null || model.DescriptionKZ == null || model.DescriptionRU == null)
            {
                throw new Exception("Все поля должны быть заполнены!");
            }

            //if (CheckRegistrationPeriod())
            //{
            int _userID = 34780;
            if (userID != null)
            {
                _userID = _authService.GetUserInfo(userID, language).Id;
            }


            var newDiplomaWork = new DiplomaWorkModel()
            {
                TopicKZ = model.TopicKZ,
                TopicRU = model.TopicRU,
                TopicEN = model.TopicEN,
                DescriptionKZ = model.DescriptionKZ,
                DescriptionRU = model.DescriptionRU,
                DescriptionEN = model.DescriptionEN,
                GraduationYearID = model.GraduationYearID,
                AddedTime = DateTime.Now,
                Amount = (DiplomaWorkType)model.TypeID == DiplomaWorkType.Individual ? 1 : model.Amount,
                TypeID = model.TypeID,
                InstructorID = _userID,
                StatusID = (int)DiplomaWorkStatus.Active
            };

            _diplomaRepository.InsertDiplomaWork(newDiplomaWork);
            //}
            //else
            //{
            //    throw new Exception("Время регистрации");
            //}
        }


        public IEnumerable<DiplomaWorkTypeModel> GetDiplomaTypes()
        {
            return _diplomaRepository.GetDiplomaTypes();
        }

        public void StudentRequest(List<CreateDiplomaWorkStudentModel> diplomaIDs, string userID, string language)
        {
            if (CheckRegistrationPeriod())
            {
                int _userID = 34780;

                if (userID != null)
                {
                    _userID = _authService.GetUserInfo(userID, language).Id;
                }

                if (_diplomaRepository.IsHaveAcceptedDiplomaWork(_userID))
                {
                    throw new Exception("Вы уже имеете Дипломную работу со статусом \"Подтвержден\"");
                }

                var requests = new List<DiplomaWorkStudentModel>();

                foreach (var diplomaID in diplomaIDs)
                {
                    if (!_diplomaRepository.IsHaveSimilarDiplomaWork(_userID, diplomaID.DiplomaID))
                    {
                        var request = new DiplomaWorkStudentModel()
                        {
                            DiplomaID = diplomaID.DiplomaID,
                            StatusID = (int)DiplomaWorkStudentStatus.New,
                            StudentID = _userID,
                            AddedTime = DateTime.Now,
                        };
                        requests.Add(request);
                    }
                }
                _diplomaRepository.StudentRequest(requests);
            }
            else
            {
                throw new Exception("Период регистрации прошел...");
            }
        }

        public void TeacherAcceptStudent(AcceptStudentModel requests)
        {
            var _diplomaWork = _diplomaRepository.GetDiplomaWorkByID(requests.DiplomaID);
            var students = _diplomaRepository.GetStudentsByDiplomaID(requests.DiplomaID);
            var filter = new DiplomaWorksCalendarFilterModel() { Year = DateTime.Now.Year };
            var periods = _calendarRepository.GetAll(filter);

            if (_diplomaWork.TypeID == (int)DiplomaWorkType.Individual)
            {
                foreach (var student in students)
                {
                    if (requests.Students.Any(x => x.StudentID == student.StudentID))
                    {
                        if (CheckStudentsCount(requests.DiplomaID, DiplomaWorkType.Individual))
                        {
                            student.StatusID = (int)DiplomaWorkStudentStatus.Accepted;
                            student.UpdatedTime = DateTime.Now;

                            var diplomaWorks = _diplomaRepository.GetStudentsByStudentID(student.StudentID);

                            foreach (var diplomaWork in diplomaWorks)
                            {
                                if (diplomaWork.DiplomaID != requests.DiplomaID)
                                {
                                    diplomaWork.StatusID = (int)DiplomaWorkStudentStatus.Canceled;
                                }
                            }
                            _diplomaRepository.UpdateManyStudents(diplomaWorks.ToList());
                            _diplomaRepository.UpdateStudents(student);
                            GenerateJournalForStudent(student.StudentID, requests.DiplomaID, DayOfWeek.Saturday, periods);
                        }
                    }
                }
            }
            if (_diplomaWork.TypeID == (int)DiplomaWorkType.Group)
            {
                foreach (var student in students)
                {
                    if (requests.Students.Any(x => x.StudentID == student.StudentID))
                    {
                        if (CheckStudentsCount(requests.DiplomaID, DiplomaWorkType.Group))
                        {
                            student.StatusID = (int)DiplomaWorkStudentStatus.Accepted;
                            student.UpdatedTime = DateTime.Now;

                            var diplomaWorks = _diplomaRepository.GetStudentsByStudentID(student.StudentID);

                            foreach (var diplomaWork in diplomaWorks)
                            {
                                if (diplomaWork.DiplomaID != requests.DiplomaID)
                                {
                                    diplomaWork.StatusID = (int)DiplomaWorkStudentStatus.Canceled;
                                }
                            }
                            _diplomaRepository.UpdateManyStudents(diplomaWorks.ToList());
                            _diplomaRepository.UpdateStudents(student);
                            GenerateJournalForStudent(student.StudentID, requests.DiplomaID, DayOfWeek.Saturday, periods);
                        }
                    }
                }
            }
        }


        private void GenerateJournalForStudent(int studentID, int diplomaID, DayOfWeek dayOfWeek, IEnumerable<DiplomaWorksCalendarResultModel> periods)
        {
            var gradingPeriod = periods.Where(x => x.RegistrationTypeID == (int)RegistrationTypes.GradingPeriod).FirstOrDefault();
            var current = gradingPeriod.StartRegistration;
            var end = gradingPeriod.EndRegistration;
            while (current <= end)
            {
                if (current.DayOfWeek == dayOfWeek)
                    break;

                current = current.AddDays(1);
            };
            var student = new CreateJournalRequestModel() { DiplomaWorkID = diplomaID, StudentID = studentID };
            _journalRepository.InsertDiplomaWorkJournal(student);
            while (current <= end)
            {
                var date = new DateModel()
                {
                    StudentID = studentID,
                    AttendanceDate = current,
                    isActive = false
                };
                _journalRepository.InsertDiplomaWorkJournalDates(date);
                current = current.AddDays(7);
            };
            var passNoPassPeriod = periods.Where(x => x.RegistrationTypeID == (int)RegistrationTypes.PassNoPassPeriod).FirstOrDefault();
            var pNpModel = new DateModelForPassNoPass()
            {
                StudentID = studentID,
                StartDate = passNoPassPeriod.StartRegistration,
                EndDate = passNoPassPeriod.EndRegistration,
                isActive = false
            };
            _journalRepository.InsertDiplomaWorkJournalDates(pNpModel);
        }

        public void TeacherRejectStudent(RejectStudentModel requests, string body, string userID, string language)
        {
            var students = _diplomaRepository.GetStudentsByDiplomaID(requests.DiplomaID);
            var header = "Отказ";
            int _userID = 34780;

            if (userID != null)
            {
                _userID = _authService.GetUserInfo(userID, language).Id;
            }

            foreach (var student in students)
            {
                if (requests.Students.Any(x => x.StudentID == student.StudentID))
                {
                    student.StatusID = (int)DiplomaWorkStudentStatus.Rejected;
                    student.UpdatedTime = DateTime.Now;
                    _diplomaRepository.UpdateStudents(student);
                }
            }
            requests.Students.ForEach(x => _notificationService.SendNotification(new NotificationForDiploma()
            {
                Body = body,
                Header = header,
                DayCount = 0,
                RecipientID = x.StudentID,
                SenderID = _userID
            }));
        }

        private bool CheckStudentsCount(int diplomaID, DiplomaWorkType diplomaWorkType)
        {
            var diplomaTopics = _diplomaRepository.CheckStudentsCount(diplomaID);
            var diplomaTopic = diplomaTopics.FirstOrDefault();
            bool result = false;
            if (diplomaTopics.Count() == 0)
            {
                if (diplomaWorkType == DiplomaWorkType.Individual)
                {
                    result = true;
                    _diplomaRepository.ChangeDiplomaWorkStatus(diplomaID, (int)DiplomaWorkStatus.NonActive, DateTime.Now);
                }
                else
                {
                    result = true;
                }
            }
            if (diplomaTopic != null)
            {
                if (diplomaTopic.Amount > diplomaTopics.Count())
                {
                    result = true;
                    if (diplomaTopic.Amount == diplomaTopics.Count() + 1)
                    {
                        if (diplomaTopic.DiplomaStatusID == (int)DiplomaWorkStatus.Active)
                        {
                            _diplomaRepository.ChangeDiplomaWorkStatus(diplomaID, (int)DiplomaWorkStatus.NonActive, DateTime.Now);
                        }
                    }
                }

                if (diplomaTopic.DiplomaStatusID == (int)DiplomaWorkStatus.NonActive)
                {
                    result = false;
                }
            }
            return result;
        }

        public IEnumerable<InstructorModel> GetAllInstructors()
        {
            return _diplomaRepository.GetAllInstructors();
        }

        private bool CheckRegistrationPeriod()
        {
            var filter = new DiplomaWorksCalendarFilterModel() { Year = DateTime.Now.Year };
            var periods = _calendarRepository.GetAll(filter);
            var result = false;
            var period = periods.Where(x => x.RegistrationTypeID == (int)RegistrationTypes.RegistrationPeriod).FirstOrDefault();
            if (period.StartRegistration < DateTime.Now && period.EndRegistration > DateTime.Now)
            {
                result = true;
            }
            return result;
        }

        public DiplomaWorkModelWithoutStudentsViewModelAndTotalRowCount GetAllDiplomaWorksWithoutStudents(DiplomasFilter filter, string language, string userID)
        {
            int _userID = 34780;

            if (userID != null)
            {
                _userID = _authService.GetUserInfo(userID, language).Id;
            }

            var diplomaTopics = _diplomaRepository.GetAllDiplomaWorksWithoutStudents(filter, language);

            var diplomaWorkIDs = diplomaTopics.DiplomaWorks.Select(topics => topics.ID).Distinct();

            var result = new DiplomaWorkModelWithoutStudentsViewModelAndTotalRowCount();
            result.DiplomaWorks = new List<DiplomaWorkWithoutStudentsViewModel>();
            result.TotalRowCount = diplomaTopics.TotalRowCount;
            var diplomaWorks = _diplomaRepository.GetAllDiplomaWorksByStudentID(_userID).ToList();
            foreach (var diplomaWorkID in diplomaWorkIDs)
            {
                var topic = diplomaTopics.DiplomaWorks.FirstOrDefault(x => x.ID == diplomaWorkID);
                var studentStatusID = diplomaWorks.FirstOrDefault(x => x.DiplomaID == diplomaWorkID);
                var resultTopic = new DiplomaWorkWithoutStudentsViewModel()
                {
                    ID = topic.ID,
                    FullName = topic.FullName,
                    Topic = topic.Topic,
                    Description = topic.Description,
                    Type = topic.Type,
                    Amount = topic.Amount,
                    GraduationYearID = topic.GraduationYearID,
                    InstructorID = topic.InstructorID,
                    StatusID = topic.StatusID,
                    DiplomaStatus = topic.DiplomaStatus,
                    StudentStatusID = studentStatusID == null ? 0 : studentStatusID.StudentStatusID
                };
                result.DiplomaWorks.Add(resultTopic);
            }
            return result;
        }
    }
}
