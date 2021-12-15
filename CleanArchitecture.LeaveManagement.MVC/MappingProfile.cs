using AutoMapper;
using CleanArchitecture.LeaveManagement.MVC.Models;
using CleanArchitecture.LeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();




            //CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVM>().ReverseMap();
            //CreateMap<LeaveRequestDto, LeaveRequestVM>()
            //    .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            //    .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            //    .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            //    .ReverseMap();
            //CreateMap<LeaveRequestListDto, LeaveRequestVM>()
            //    .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            //    .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            //    .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            //    .ReverseMap();
            //CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
            //CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();
            //CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
    
}
