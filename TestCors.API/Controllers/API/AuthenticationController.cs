using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestCors.DTO;
using TestCors.Services.Core;

namespace TestCors.API.Controllers.API
{
    [Route("api/v1/auth")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthService _authenticationService;

        public AuthenticationController(IAuthService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return ReturnResult(await _authenticationService.LoginAsync(model));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return ReturnResult(await _authenticationService.RegisterAsync(model));
        }
    }
}