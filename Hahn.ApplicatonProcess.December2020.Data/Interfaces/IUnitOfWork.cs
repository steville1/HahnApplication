using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Data.Interfaces
{
    public interface IUnitOfWork
    {
        HahnApplicationDBContext Context { get; }
        IApplicant Applicant { get; }
        
    }
}
