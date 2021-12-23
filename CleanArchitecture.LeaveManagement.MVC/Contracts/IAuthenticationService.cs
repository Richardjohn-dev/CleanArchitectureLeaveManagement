using CleanArchitecture.LeaveManagement.MVC.Models;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVM registration);
        Task Logout();
    }
}
