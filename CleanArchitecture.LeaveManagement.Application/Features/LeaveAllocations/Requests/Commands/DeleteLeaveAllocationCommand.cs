using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands
{
    public class DeleteLeaveAllocationCommand : IRequest
    {
        public int Id { get; set; }
    }
}
