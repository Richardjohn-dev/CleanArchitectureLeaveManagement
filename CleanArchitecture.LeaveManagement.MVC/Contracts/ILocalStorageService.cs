using System.Collections.Generic;

namespace CleanArchitecture.LeaveManagement.MVC.Contracts
{
    // after token issued, we need to store this.
    // stores JWT token between operations
    public interface ILocalStorageService
    {
        void ClearStorage(List<string> keys);
        bool Exists(string key);
        T GetStorageValue<T>(string key);
        void SetStorageValue<T>(string key, T value);
    }
}