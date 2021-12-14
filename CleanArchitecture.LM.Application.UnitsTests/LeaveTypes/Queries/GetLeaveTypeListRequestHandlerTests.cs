using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.LeaveManagement.Application.Contracts.Persistence;
using Moq;
using CleanArchitecture.LM.Application.UnitsTests.Mocks;
using CleanArchitecture.LeaveManagement.Application.Profiles;
using Xunit;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using CleanArchitecture.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using System.Threading;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveType;
using Shouldly;

namespace CleanArchitecture.LM.Application.UnitsTests.LeaveTypes.Queries
{
    public class GetLeaveTypeListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        public GetLeaveTypeListRequestHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            // our assertion
            result.ShouldBeOfType<List<LeaveTypeDto>>();

            result.Count.ShouldBe(3);
        }
    }
}
