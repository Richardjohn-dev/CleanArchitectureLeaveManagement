using CleanArchitecture.LeaveManagement.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leavetypeRepository;

        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leavetypeRepository)
        {
            _leavetypeRepository = leavetypeRepository;

            Include(new ILeaveAllocationDtoValidator(_leavetypeRepository));

        }
    }
}
