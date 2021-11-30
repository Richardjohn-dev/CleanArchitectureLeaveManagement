using AutoMapper;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveAllocation;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveRequest;
using CleanArchitecture.LeaveManagement.Application.DTOs.LeaveType;
using CleanArchitecture.LeaveManagement.Domain;

namespace CleanArchitecture.LeaveManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        }
    }
}
