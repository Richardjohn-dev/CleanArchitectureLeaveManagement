using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveType.Validators;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
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

namespace CleanArchitecture.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var validator = new CreateLeaveTypeDtoValidator();          
            var validationResults = await validator.ValidateAsync(request.CreateLeaveTypeDto, cancellationToken);
            if (validationResults.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResults.Errors.Select(q => q.ErrorMessage).ToList();
                //   throw new ValidationException(validationResults);
            }
            else
            {
                var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
                leaveType = await _unitOfWork.LeaveTypeRepository.AddAsync(leaveType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveType.Id;
            }

            return response;

        }
    }
}
