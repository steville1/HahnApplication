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
    public class ApplicantRepository : RepositoryBase<Applicant>, IApplicant
    {
        public ApplicantRepository(HahnApplicationDBContext context)
            : base(context)
        {
        }

       //If there is any method that is specific to just ApplicantRepository Implement method here

        
    }
}