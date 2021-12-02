﻿using CleanArchitecture.LeaveManagement.Application.Persistence.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveAllocationRepository _leaveTypeRepository;
        private ILeaveTypeRepository _leaveTypeRepository1;

        public UpdateLeaveRequestDtoValidator(ILeaveAllocationRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }

        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository1)
        {
            _leaveTypeRepository1 = leaveTypeRepository1;
        }
    }
}