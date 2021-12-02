using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using CleanArchitecture.LeaveManagement.Application.Persistence.Contracts;
using CleanArchitecture.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResults = await validator.ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);

            if (validationResults.IsValid == false)
                throw new Exception();

            var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);
            return leaveRequest.Id;
        }
    }
}
