using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
    public class ApplicantResponseMessage
    {
        public IList<ValidationFailure> Errors { get; set; }
        public string ResponseMessage { get; set; }

    }
}
