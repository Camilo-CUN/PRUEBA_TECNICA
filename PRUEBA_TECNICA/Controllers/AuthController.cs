using Microsoft.AspNetCore.Mvc;

namespace PRUEBA_TECNICA.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginModel)
		{
			var token = await _authService.Authenticate(loginModel.Email, loginModel.Password);
			if (token == null)
			{
				return Unauthorized();
			}
			return Ok(new { Token = token });
		}
	}

	public class LoginRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
