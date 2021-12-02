using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using CleanArchitecture.LeaveManagement.Application.Exceptions;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using CleanArchitecture.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validationResults = await validator.ValidateAsync(request.UpdateLeaveAllocationDto, cancellationToken);

            if (validationResults.IsValid == false)
                throw new ValidationException(validationResults);

            var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.UpdateLeaveAllocationDto.Id);
            _mapper.Map(request.UpdateLeaveAllocationDto, leaveAllocation);

            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
