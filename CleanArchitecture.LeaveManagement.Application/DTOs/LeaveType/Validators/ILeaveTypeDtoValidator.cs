using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDtoValidator : AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeDtoValidator()
        {
            // name
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} most not exceed 50 characters.");

            // default days
            RuleFor(n => n.DefaultDays)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} must be at least 1")
               .LessThan(100).WithMessage("{PropertyName} must be less than 100");
        }
    }
}
