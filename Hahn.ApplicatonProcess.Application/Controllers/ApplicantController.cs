


using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Application.Controllers
{
    
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _applicantService;
        private readonly ILogger<ApplicantController> _logger;
        public ApplicantController(IApplicantService applicantService, ILogger<ApplicantController> logger)
        {
            _applicantService = applicantService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("AddApplicant")]
        public async Task<IActionResult> AddApplicant([FromBody]ApplicantResource resource)
        {
            try
            {
               
                var result =await _applicantService.AddApplicant(resource);
                if (result.ResponseMessage != "Successfully Saved" && result.Errors != null)
                {
                    ModelState.AddModelError("", "Something went wrong when adding the record ");
                    return StatusCode(400, ModelState);
                }
                if (result.Errors != null)
                {
                    return BadRequest(result.Errors);
                }
                
                return StatusCode(201, result.ResponseMessage);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                throw ex;
            }
        }
        [Route("GetAllApplicants/")]
        [HttpGet]
        public async Task<IActionResult> GetAllApplicants()
        {
            try
            {
                var results = await _applicantService.GetAllApplicant();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                throw ex;
            }

        }
        [Route("GetApplicantById/{applicantid}")]
        [HttpGet]
        public IActionResult GetApplicantById(string applicantid)
        {
            try
            {
                var result = _applicantService.GetApplicantById(applicantid);
                if (result != null)
                {
                    return Ok(result.Result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                throw ex;
            }
        }
        [HttpPut("UpdateApplicant/{Id}")]
        public async Task<IActionResult> UpdateApplicant(string Id, [FromBody] ApplicantResource resource)
        {
            try
            {

                var result = await _applicantService.UpdateApplicant(Id,resource);
                if (result.ResponseMessage != "Successfully Updated" && result.ResponseMessage != "Entity To Update Can Not Be Found" && result.Errors != null)
                {
                    ModelState.AddModelError("", "Something went wrong when updating the record ");
                    return StatusCode(400, ModelState);
                }
                if (result.Errors != null)
                {
                    return BadRequest(result.Errors);
                }

                return StatusCode(201, result.ResponseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                throw ex;
            }
        }
        [HttpDelete("DeleteApplicant/{Id}")]
        public async Task<IActionResult> DeleteApplicant(string Id)
        {
            try
            {

                var result = await _applicantService.DeleteApplicant(Id);
                if (result.ResponseMessage != "Successfully Deleted" && result.ResponseMessage != "Entity To Delete Can Not Be Found")
                {
                    ModelState.AddModelError("", "Something went wrong when deleting the record ");
                    return StatusCode(400, ModelState);
                }
                if(result.ResponseMessage== "Entity To Delete Can Not Be Found")
                {
                    return NotFound();
                }
                return StatusCode(201, result.ResponseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                throw ex;
            }
        }
    }
}
