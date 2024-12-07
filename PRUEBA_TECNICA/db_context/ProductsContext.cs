using Microsoft.EntityFrameworkCore;
using PRUEBA_TECNICA.Models;

namespace PRUEBA_TECNICA.db_context
{
	public class ProductsContext : DbContext // Cambié el nombre de DbContext a ProductsContext
	{
		public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { }

		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categorys { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Configuraciones adicionales para el modelo, si es necesario
		}
	}
}
