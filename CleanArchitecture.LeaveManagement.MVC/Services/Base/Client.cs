using System.Net.Http;

namespace CleanArchitecture.LeaveManagement.MVC.Services.Base
{
    public partial class Client : IClient // partial class combines together
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient; // comes from same namespace ServiceClient.cs
            }
        }
    }
}
