using eShopSolution.ViewModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Service
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
    }
}
