using Microsoft.EntityFrameworkCore;
using PRUEBA_TECNICA.db_context;
using PRUEBA_TECNICA.Models;

namespace PRUEBA_TECNICA.services
{
	public interface IProductDbService
	{
		Task<IEnumerable<ProductModel>> GetAllProductsAsync();

		Task<bool> CreateProductAsync(ProductModel product);

		Task<bool> DeleteProductByIDAsync(int id);

		Task<bool> UpdateProductAsync(int id, ProductModel updatedProduct);

		Task<IEnumerable<ProductModel>> GetProductOfCategories(string category);

		Task<int?> GetTotalProductStock();

	}
	public class ProductDbService : IProductDbService
	{
		private readonly ProductsContext _productsContext;

		public ProductDbService(ProductsContext productsContext)
		{
			_productsContext = productsContext;
		}

		/// <summary>
		/// Función para retornar todos los productos
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
		{
			return await _productsContext.Products.ToListAsync();
		}

		/// <summary>
		/// Función para crear productos
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		public async Task<bool> CreateProductAsync(ProductModel product)
		{
			try
			{
				await _productsContext.Products.AddAsync(product);
				await _productsContext.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Endpoint Eliminar Producto
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<bool> DeleteProductByIDAsync(int id)
		{
			var product = await _productsContext.Products.FirstOrDefaultAsync(u => u.Id == id);
			if (product == null)
			{
				return false;
			}

			_productsContext.Products.Remove(product);
			await _productsContext.SaveChangesAsync();

			return true;
		}

		/// <summary>
		/// ACTUALIZAR PRODUCTOS 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedProduct"></param>
		/// <returns></returns>
		public async Task<bool> UpdateProductAsync(int id, ProductModel updatedProduct)
		{
			try
			{
				// Buscar al usuario por el Id
				var existingProduct = await _productsContext.Products.FirstOrDefaultAsync(u => u.Id == id);

				// Si no se encuentra el usuario, retornar falso
				if (existingProduct == null)
				{
					return false; // El usuario no existe
				}

				// Actualizar los campos del usuario
				existingProduct.Name = updatedProduct.Name ?? existingProduct.Name;
				existingProduct.Description = updatedProduct.Description ?? existingProduct.Description;
				existingProduct.Foto = updatedProduct.Foto ?? existingProduct.Foto;
				existingProduct.category = updatedProduct.category ?? existingProduct.category;
				existingProduct.amount = updatedProduct.amount ?? existingProduct.amount;
				existingProduct.price = updatedProduct.price ?? existingProduct.price;

				// Guardar los cambios en la base de datos
				await _productsContext.SaveChangesAsync();

				return true; // Usuario actualizado correctamente
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false; // Error al actualizar el usuario
			}
		}


		/// <summary>
		/// Obtener productos por categoria
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		public async Task<IEnumerable<ProductModel>> GetProductOfCategories(string category)
		{
			var products = await _productsContext.Products
				.Where(x => x.category == category)
				.ToListAsync();

			return products;
		}


		/// <summary>
		/// Obtener Total de stock por sp
		/// </summary>
		/// <returns></returns>
		public async Task<int?> GetTotalProductStock()
		{
			var totalStock = await _productsContext.Products
				.SumAsync(p => p.amount);  

			return totalStock;
		}
	}
}
