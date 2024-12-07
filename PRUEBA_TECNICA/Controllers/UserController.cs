using Microsoft.AspNetCore.Mvc;
using PRUEBA_TECNICA.services;

namespace PRUEBA_TECNICA.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		
		private readonly ILogger<UserController> _logger;

		private readonly IUserDbService _userDbService;

		public UserController(ILogger<UserController> logger, IUserDbService iuserDbService)
		{
			_logger = logger;
			_userDbService = iuserDbService;	
		}

		[HttpGet(Name = "GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			try
			{
				var users = await _userDbService.GetAllUsers();

				if (users.Any())
				{
					return BadRequest ("No se encontraron usuarios");
				}

				return Ok(users);
			}
			catch (Exception ex) {
				return BadRequest("No se encontraron usuarios");
			}
		}	
	}
}
