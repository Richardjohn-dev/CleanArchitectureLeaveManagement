using CleanArchitecture.LeaveManagement.Application.Models.Identity;
using System.Threading.Tasks;

namespace CleanArchitecture.LeaveManagement.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);

    }
}
