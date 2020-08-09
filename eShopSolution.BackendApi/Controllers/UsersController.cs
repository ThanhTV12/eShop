using eShopSolution.Application.System.User;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModel.System.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody]LoginRequest request) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var token = await _userService.Authenticate(request);
            if (token == null) 
            {
                throw new EShopException("Username or password invalid");
            }
            return Ok(token);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var isSuccess = await _userService.Register(request);
            if (isSuccess == false) 
            {
                return BadRequest("register error");
            }
            return Ok("Successfully");
        }
    }
}