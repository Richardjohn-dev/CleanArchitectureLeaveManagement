using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var err in validationResult.Errors)
            {
                Errors.Add(err.ErrorMessage);
            }
        }
    }
}
