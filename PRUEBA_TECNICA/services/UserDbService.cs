using Microsoft.EntityFrameworkCore;
using PRUEBA_TECNICA.db_context;
using PRUEBA_TECNICA.Models;

namespace PRUEBA_TECNICA.services
{
	public interface IUserDbService
	{
		Task<IEnumerable<UserModel>> GetAllUsers();
	}
	public class UserDbService: IUserDbService
	{
		private readonly dbContext _Context;

		public UserDbService(dbContext context) { 
			_Context = context;
		}

		public async Task<IEnumerable<UserModel>> GetAllUsers()
		{
			return await _Context.Users
				.ToListAsync();
		}
	}
}
