using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Models.ViewModels
{
    public class DiplomaWorkViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int StatusID { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InstructorID { get; set; }
        public int GraduationYearID { get; set; }
        public int StudentID { get; set; }
        public string ShortName { get; set; }
        public string Status { get; set; }
        public int StudentStatusID { get; set; }
        public string DiplomaStatus { get; set; }
    }

    public class DiplomaWorkViewModelWithoutStudents
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int StatusID { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InstructorID { get; set; }
        public int GraduationYearID { get; set; }
        public string DiplomaStatus { get; set; }
    }

    public class DiplomaWorkViewModelWithTotalRowCount
    {
        public List<DiplomaWorkViewModel> DiplomaWorks { get; set; }
        public int TotalRowCount { get; set; }
    }

    public class DiplomaWorkViewModelWithoutStudents_WithTotalRowCount
    {
        public List<DiplomaWorkViewModelWithoutStudents> DiplomaWorks { get; set; }
        public int TotalRowCount { get; set; }
    }

    public class DiplomaWorkWithStudentsViewModel : DiplomaWorkWithoutStudentsViewModel
    {
        public IEnumerable<DiplomaWorksStudentsViewModel> SubmittedStudents { get; set; }
        public IEnumerable<DiplomaWorksStudentsViewModel> WaitingStudents { get; set; }
    }

    public class DiplomaWorkWithoutStudentsViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InstructorID { get; set; }
        public int GraduationYearID { get; set; }
        public int StatusID { get; set; }
        public string DiplomaStatus { get; set; }
        public int StudentStatusID { get; set; }
    }

    public class DiplomaWorkModelWithStudentsViewModelAndTotalRowCount
    {
        public List<DiplomaWorkWithStudentsViewModel> DiplomaWorks { get; set; }
        public int TotalRowCount { get; set; }
    }

    public class DiplomaWorkModelWithoutStudentsViewModelAndTotalRowCount
    {
        public List<DiplomaWorkWithoutStudentsViewModel> DiplomaWorks { get; set; }
        public int TotalRowCount { get; set; }
    }
}
