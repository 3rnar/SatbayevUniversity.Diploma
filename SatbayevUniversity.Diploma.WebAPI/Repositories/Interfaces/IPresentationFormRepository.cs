using SatbayevUniversity.Diploma.WebAPI.Models.PresentationForm;
using System.Collections.Generic;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public interface IPresentationFormRepository
    {
        Protocol GetProtocolByStudentID(int studentID);
        void InsertOrUpdateProtocols(List<Protocol> protocols);
    }
}