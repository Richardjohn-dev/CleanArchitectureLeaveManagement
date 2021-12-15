using System.Net.Http;

namespace CleanArchitecture.LeaveManagement.MVC.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

    }
}
