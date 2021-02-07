using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogic
{
    public class UnitOfWork : IUnitOfWork
    {
        private HahnApplicationDBContext _context;
        private IApplicant _applicant;
        public UnitOfWork(HahnApplicationDBContext context)
        {
            _context = context;
        }

        public IApplicant Applicant
        {
            get
            {

                if (this._applicant == null)
                {
                    this._applicant = new ApplicantRepository(_context);
                }
                return _applicant;
            }
        }
      
        public HahnApplicationDBContext Context
        {
            get
            {

                if (this._context == null)
                {
                    this._context = new HahnApplicationDBContext();
                }
                return _context;
            }

        }
    }
}
