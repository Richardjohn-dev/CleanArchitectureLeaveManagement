using CleanArchitecture.LeaveManagement.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(n => n.NumberOfDays)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} must be at least 1")
               .LessThan(100).WithMessage("{PropertyName} must be less than 100");

            RuleFor(n => n.Period)
              .NotEmpty().WithMessage("{PropertyName} is required")
              .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

            // must be valid in system
            RuleFor(n => n.LeaveTypeId)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} must be at least 1")
               .MustAsync(async (id, token) =>
               {
                   var leaveTypeExists = await _leaveTypeRepository.ExistsAsync(id);
                   return !leaveTypeExists;
               })
              .WithMessage("{PropertyName} does not exist.");

          

        }
    }
}
