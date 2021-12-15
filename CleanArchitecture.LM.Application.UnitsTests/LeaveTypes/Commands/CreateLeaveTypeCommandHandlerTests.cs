using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.Contracts.Persistence;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveType;
using CleanArchitecture.LeaveManagement.Application.Exceptions;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using CleanArchitecture.LeaveManagement.Application.Profiles;
using CleanArchitecture.LeaveManagement.Application.Responses;
using CleanArchitecture.LM.Application.UnitsTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.LM.Application.UnitsTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly CreateLeaveTypeDto _createLeaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

            _createLeaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test DTO"
            };
        }

        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(
                new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockRepo.Object.GetAllAsync();

            result.ShouldBeOfType<BaseCommandResponse>();

            leaveTypes.Count.ShouldBe(4);
        }

        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            _createLeaveTypeDto.DefaultDays = -1;

            var result = await _handler.Handle(
                 new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockRepo.Object.GetAllAsync();

            leaveTypes.Count.ShouldBe(3);

            result.ShouldBeOfType<BaseCommandResponse>();
            
        }
    }
}
