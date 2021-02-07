using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public interface IApplicantService
    {
        Task<ApplicantResponseMessage> AddApplicant(ApplicantResource resource);
        Task<IEnumerable> GetAllApplicant();
        Task <Applicant>GetApplicantById(string Id);
        Task<ApplicantResponseMessage> UpdateApplicant(string id, ApplicantResource resource);
        Task<ApplicantResponseMessage> DeleteApplicant(string id);
    }
}
