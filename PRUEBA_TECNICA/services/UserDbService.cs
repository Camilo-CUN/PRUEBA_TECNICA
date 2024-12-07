using Microsoft.EntityFrameworkCore;
using PRUEBA_TECNICA.db_context;
using PRUEBA_TECNICA.Models;

namespace PRUEBA_TECNICA.services
{
	/// <summary>
	/// Interfaz de UserDbService
	/// </summary>
	public interface IUserDbService
	{
		Task<IEnumerable<UserModel>> GetAllUsers();

		Task<bool> DeleteUserByEmailAsync(string email);

		Task<bool> CreateUserAsync(UserModel user);

		Task<bool> UpdateUserAsync(int id, UserModel updatedUser);

		Task<UserModel> GetUserByEmailAndPasswordAsync(string email, string password);
	}

	public class UserDbService: IUserDbService
	{
		private readonly UsersContext _Context;

		public UserDbService(UsersContext context) { 
			_Context = context;
		}

		public async Task<IEnumerable<UserModel>> GetAllUsers()
		{
			return await _Context.Users
				.ToListAsync();
		}

		public async Task<bool> DeleteUserByEmailAsync(string email)
		{
			var user = await _Context.Users.FirstOrDefaultAsync(u => u.email == email);
			if (user == null)
			{
				return false; 
			}

			_Context.Users.Remove(user); 
			await _Context.SaveChangesAsync(); 

			return true; 
		}

		/// <summary>
		/// CREAR USUARIO FUNCTION
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<bool> CreateUserAsync(UserModel user)
		{
			try
			{
				await _Context.Users.AddAsync(user);
				await _Context.SaveChangesAsync();

				return true; 
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// ACTUALIZAR USUARIO
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedUser"></param>
		/// <returns></returns>
		public async Task<bool> UpdateUserAsync(int id, UserModel updatedUser)
		{
			try
			{
				// Buscar al usuario por el Id
				var existingUser = await _Context.Users.FirstOrDefaultAsync(u => u.Id == id);

				// Si no se encuentra el usuario, retornar falso
				if (existingUser == null)
				{
					return false; // El usuario no existe
				}

				// Actualizar los campos del usuario
				existingUser.Name = updatedUser.Name ?? existingUser.Name;
				existingUser.email = updatedUser.email ?? existingUser.email;
				existingUser.password = updatedUser.password ?? existingUser.password;
				existingUser.rol = updatedUser.rol ?? existingUser.rol;

				// Guardar los cambios en la base de datos
				await _Context.SaveChangesAsync();

				return true; // Usuario actualizado correctamente
			}
			catch (Exception ex)
			{
				// Manejo de errores
				return false; // Error al actualizar el usuario
			}
		}

		public async Task<UserModel> GetUserByEmailAndPasswordAsync(string email, string password)
		{
			return await _Context.Users
				.FirstOrDefaultAsync(u => u.email == email && u.password == password);
		}

	}
}
