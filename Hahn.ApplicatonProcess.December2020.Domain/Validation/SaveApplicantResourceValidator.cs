using FluentValidation;
using FluentValidation.Validators;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Validation
{
    public class SaveApplicantResourceValidator : AbstractValidator<ApplicantResource>
    {

        public SaveApplicantResourceValidator()
        {
            RuleFor(applicant => applicant.Name).NotNull().MinimumLength(5);
            RuleFor(applicant => applicant.FamilyName).NotNull().MinimumLength(5);
            RuleFor(applicant => applicant.Address).NotNull().MinimumLength(10);
            RuleFor(applicant => applicant.EMailAdress).NotNull().EmailAddress();
            RuleFor(applicant => applicant.Age).InclusiveBetween(20, 60);
            RuleFor(applicant => applicant.Hired).NotNull();
            RuleFor(app => app.CountryOfOrigin).NotNull().SetValidator(new CountryValidator());
        }

    }
    public class CountryValidator : AsyncValidatorBase
    {

        public CountryValidator() : base("Country name '{PropertyValue}' is not valid.")
        {

        }
        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            using (var _httpclient = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var get = await _httpclient.GetAsync($"https://restcountries.eu/rest/v2/name/{context.PropertyValue}?fullText=true");
                return get.IsSuccessStatusCode;
            }

        }
    }
}
