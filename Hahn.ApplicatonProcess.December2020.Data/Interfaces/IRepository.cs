using Hahn.ApplicatonProcess.December2020.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Interfaces
{
    public interface IRepository<T> where T : BusinessEntity
    {
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task Delete(T entity);
        Task<T> FindByID(object ID);
        Task <IEnumerable<T>> FindAll();
    }
}
