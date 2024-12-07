using Microsoft.AspNetCore.Mvc;
using PRUEBA_TECNICA.Models;
using PRUEBA_TECNICA.services;

namespace PRUEBA_TECNICA.Controllers
{
	[ApiController]
	[Route("Api/[controller]")]
	public class UserController : ControllerBase
	{
		
		private readonly ILogger<UserController> _logger;

		private readonly IUserDbService _userDbService;

		public UserController(ILogger<UserController> logger, IUserDbService iuserDbService)
		{
			_logger = logger;
			_userDbService = iuserDbService;	
		}

		//ENDPOINT OBTENER LOS USUARIOS
		[HttpGet]
		[Route("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			try
			{
				var users = await _userDbService.GetAllUsers();

				if (!users.Any())
				{
					return BadRequest ("No se encontraron usuarios");
				}

				return Ok(users);
			}
			catch (Exception ex) {
				return BadRequest("No se encontraron usuarios" + ex);
			}
		}

		/// <summary>
		///	ENDPOINT para eliminar usuarios
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("DeleteUserByEmail/{email}")]
		public async Task<IActionResult> DeleteUserByEmail(string email)
		{
			try
			{
				var isDeleted = await _userDbService.DeleteUserByEmailAsync(email);

				if (!isDeleted)
				{
					return NotFound($"No se encontró el usuario con el email: {email}");
				}

				return Ok($"Usuario con email {email} eliminado correctamente.");
			}
			catch (Exception ex)
			{
				return BadRequest($"Hubo un problema al eliminar el usuario: {ex.Message}");
			}
		}

		/// <summary>
		/// ENDPOINT Creacion de usuarios
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("CreateUser")]
		public async Task<IActionResult> CreateUser([FromBody] UserModel user)
		{
			try
			{
				// Verificar que los campos necesarios no sean nulos
				if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.rol))
				{
					return BadRequest("Todos los campos son requeridos");
				}

				// Llamar al servicio para crear el usuario
				var isCreated = await _userDbService.CreateUserAsync(user);

				if (isCreated)
				{
					return Ok("Usuario creado exitosamente.");
				}
				else
				{
					return BadRequest("Hubo un problema al crear el usuario.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Ocurrió un error al crear el usuario: " + ex.Message);
			}
		}

		/// <summary>
		/// ENDPOINT ACTUALIZAR USUARIO
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedUser"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("UpdateUser/{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel updatedUser)
		{
			try
			{
				// Llamar al servicio para actualizar el usuario
				var isUpdated = await _userDbService.UpdateUserAsync(id, updatedUser);

				// Si el usuario no fue encontrado, retornar NotFound
				if (!isUpdated)
				{
					return NotFound($"No se encontró el usuario con el ID {id}");
				}

				// Si todo salió bien, retornar Ok
				return Ok($"Usuario con ID {id} actualizado correctamente.");
			}
			catch (Exception ex)
			{
				// En caso de error, retornar BadRequest con el mensaje de la excepción
				return BadRequest($"Hubo un problema al actualizar el usuario: {ex.Message}");
			}
		}
	}
}
