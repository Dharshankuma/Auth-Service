using Authservice.Application.DTO;
using Authservice.Application.Interface;
using Microsoft.AspNetCore.Mvc;


namespace Auth_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;
        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO objdto)
        {
            var result = await _authService.DoRegisterUser(objdto);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objdto)
        {
            var result = await _authService.Login(objdto);
            return Ok(result);

        }
    }
}