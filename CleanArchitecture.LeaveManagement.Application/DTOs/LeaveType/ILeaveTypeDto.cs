namespace CleanArchitecture.LeaveManagement.Application.DTOs.LeaveType
{
    public interface ILeaveTypeDto
    {
      public int DefaultDays { get; set; }
      public string Name { get; set; }
    }
}