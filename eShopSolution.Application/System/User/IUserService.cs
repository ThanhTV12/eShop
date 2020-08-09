using System.Threading.Tasks;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModel.System.User;

namespace eShopSolution.Application.System.User
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(UserRegisterRequest request);
    }
}