using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PRUEBA_TECNICA.Models;

namespace PRUEBA_TECNICA.db_context
{
	public class dbContext : DbContext
	{
		public class ProductsContext(DbContextOptions<DbContext> options) { }

		public DbSet<ProductModel> Products { get; set; }		
		public DbSet<UserModel> Users { get; set; }		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales para el modelo, si es necesario
        }
	}
}
