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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO objdto)
        {
            try
            {
                var result = await _authService.DoRegisterUser(objdto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
