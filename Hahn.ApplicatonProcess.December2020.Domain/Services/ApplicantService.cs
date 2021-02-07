
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public ApplicantService(IUnitOfWork unitOfWork, ILogger<ApplicantService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApplicantResponseMessage> AddApplicant(ApplicantResource resource)
        {
            try
            {
                var validation = new SaveApplicantResourceValidator();

                var validationResult = validation.Validate(resource);
                if (!validationResult.IsValid)
                {
                    return new ApplicantResponseMessage { Errors = validationResult.Errors };
                }
               
                var applicant = new Applicant();
                applicant.Name = resource.Name;
                applicant.Address = resource.Address;
                applicant.Age = resource.Age;
                applicant.CountryOfOrigin = resource.CountryOfOrigin;
                applicant.EMailAdress = resource.EMailAdress;
                applicant.FamilyName = resource.FamilyName;
                applicant.Hired = resource.Hired;
                await _unitOfWork.Applicant.SaveAsync(applicant);
                return new ApplicantResponseMessage { ResponseMessage = "Successfully Saved" };
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }

        }

        public async Task<ApplicantResponseMessage> DeleteApplicant(string id)
        {
            try
            {
                var findId = await GetApplicantById(id);
                
                if (findId != null)
                {
                    var result = _unitOfWork.Applicant.Delete(findId);
                    return new ApplicantResponseMessage { ResponseMessage = "Successfully Deleted" };
                }
                else
                {
                    return new ApplicantResponseMessage { ResponseMessage = "Entity To Delete Can Not Be Found" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable> GetAllApplicant()
        {
            try
            {
                var result = await _unitOfWork.Applicant.FindAll();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }
        }

        public async Task<Applicant> GetApplicantById(string Id)
        {
            try
            {
                string id = Id;
                int applicantId = Int32.Parse(id);
                var result=  await _unitOfWork.Applicant.FindByID(applicantId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }
        }

        public async Task<ApplicantResponseMessage> UpdateApplicant(string id, ApplicantResource resource)
        {
            try
            {
                var validation = new SaveApplicantResourceValidator();
                var findId = await GetApplicantById(id);
                if (findId != null)
                {
                    var validationResult = validation.Validate(resource);
                    if (!validationResult.IsValid)
                    {
                        return new ApplicantResponseMessage { Errors = validationResult.Errors };
                    }

                    //var applicant = new Applicant();
                    findId.Name = resource.Name;
                    findId.Address = resource.Address;
                    findId.Age = resource.Age;
                    findId.CountryOfOrigin = resource.CountryOfOrigin;
                    findId.EMailAdress = resource.EMailAdress;
                    findId.FamilyName = resource.FamilyName;
                    findId.Hired = resource.Hired;
                    await _unitOfWork.Applicant.UpdateAsync(findId);
                    return new ApplicantResponseMessage { ResponseMessage = "Successfully Updated" };
                }
                else
                {
                    return new ApplicantResponseMessage { ResponseMessage = "Entity To Update Can Not Be Found" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw ex;
            }
        }
    }
}
