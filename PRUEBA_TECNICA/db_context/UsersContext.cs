using Microsoft.EntityFrameworkCore;
using PRUEBA_TECNICA.Models;

namespace PRUEBA_TECNICA.db_context
{
	public class UsersContext : DbContext 
	{
		public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }

		public DbSet<UserModel> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
