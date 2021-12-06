﻿using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using CleanArchitecture.LeaveManagement.Application.Exceptions;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using CleanArchitecture.LeaveManagement.Application.Contracts.Persistence;
using CleanArchitecture.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validationResults = await validator.ValidateAsync(request.CreateLeaveAllocationDto, cancellationToken);

            if (validationResults.IsValid == false)
                throw new ValidationException(validationResults);

            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
            leaveAllocation = await _leaveAllocationRepository.AddAsync(leaveAllocation);
            return leaveAllocation.Id;
        }
    }
}
