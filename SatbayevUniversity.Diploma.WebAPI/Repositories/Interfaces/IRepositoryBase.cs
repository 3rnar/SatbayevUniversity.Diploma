using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Delete(T entity);
        void Insert(T entity);
        IEnumerable<T> Query(string where = null);
        T SingleQuery(int id);
        void Update(T entity);
    }
}
