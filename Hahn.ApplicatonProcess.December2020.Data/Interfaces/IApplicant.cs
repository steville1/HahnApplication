using Hahn.ApplicatonProcess.December2020.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Interfaces
{
    public interface IApplicant : IRepository<Applicant>
    {
      //The is to allow extensibility, All shareable methods  have been declared on IRepository Interface
     //If there is any method that is specific to Just APplicant declare method here
    }
}
