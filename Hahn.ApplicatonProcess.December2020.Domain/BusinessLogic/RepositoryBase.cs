using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogic
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BusinessEntity, new()
    {
        protected HahnApplicationDBContext _context { get; set; }

        public RepositoryBase(HahnApplicationDBContext context)
        {
            this._context = context;

        }
        public virtual async Task SaveAsync(T entity)
        {
            this._context.Set<T>().Add(entity);
            await this._context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
           await  _context.SaveChangesAsync();
        }

        public virtual async Task<T> FindByID(object ID)
        {
           return await _context.Set<T>().FindAsync(ID);      
        }
        public async  Task<IEnumerable<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
