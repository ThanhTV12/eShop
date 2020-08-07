using System.Threading.Tasks;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModel.System.User;
using Microsoft.AspNetCore.Identity;
using NotImplementedException = System.NotImplementedException;

namespace eShopSolution.Application.System.User
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;


        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public Task<string> Authenticate(LoginRequest request)
        {
            var user = _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new EShopException("user or password invalid");
            }
            
        }

        public Task<bool> Register(UserRegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}