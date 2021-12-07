using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using CleanArchitecture.LeaveManagement.Application.Contracts.Persistence;
using CleanArchitecture.LeaveManagement.Application.Exceptions;
using CleanArchitecture.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.LeaveManagement.Application.Responses;
using CleanArchitecture.LeaveManagement.Application.Contracts.Infrastructure;
using CleanArchitecture.LeaveManagement.Application.Models;

namespace CleanArchitecture.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);

            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveRequest.Id;

            var email = new Email
            {
                To = "employee@org.com",
                Body = $"Your leave request for {request.CreateLeaveRequestDto.StartDate:D} to {request.CreateLeaveRequestDto.EndDate:D} " +
                $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };
            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception)
            {
                //// Log or handle error, but don't throw...
            }

            return response;
        }
    }
}
