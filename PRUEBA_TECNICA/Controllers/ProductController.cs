using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PRUEBA_TECNICA.Models;
using PRUEBA_TECNICA.services;

namespace PRUEBA_TECNICA.Controllers
{
	[Authorize]
	[ApiController]
	[Route("Api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductDbService _productDbService;

		public ProductController(IProductDbService productDbService)
		{
			_productDbService = productDbService;
		}

		//ENDPOINT OBTENER LOS PRODUCTOS
		[HttpGet]
		[Route("GetAllproducts")]

		public async Task<IActionResult> GetAllProducts()
		{
			try
			{
				var products = await _productDbService.GetAllProductsAsync();

				if (!products.Any())
				{
					return BadRequest("No se encontraron productos");
				}

				return Ok(products);
			}
			catch (Exception ex)
			{
				return BadRequest("No se encontraron productos" + ex);
			}
		}

		//ENDPOINT OBTENER LOS PRODUCTOS
		[HttpGet]
		[Route("GetProductsOfCategory")]

		public async Task<IActionResult> GetProductsOfCategory(string category)
		{
			try
			{
				var products = await _productDbService.GetProductOfCategories(category);

				if (!products.Any())
				{
					return BadRequest("No se encontraron productos para este filtro");
				}

				return Ok(products);
			}
			catch (Exception ex)
			{
				return BadRequest("No se encontraron productos" + ex);
			}
		}

		//ENDPOINT OBTENER LOS PRODUCTOS
		[HttpGet]
		[Route("GetTotalStock")]

		public async Task<IActionResult> GetTotalStock()
		{
			try
			{
				var stock = await _productDbService.GetTotalProductStock();

				if (stock <= 0)
				{
					return BadRequest("No se encontraron productos para este filtro");
				}

				return Ok(stock);
			}
			catch (Exception ex)
			{
				return BadRequest("No se encontraron productos" + ex);
			}
		}

		/// <summary>
		///	ENDPOINT para eliminar productos
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("DeleteProductById/{id}")]
		public async Task<IActionResult> DeleteUserByEmail(int id)
		{
			try
			{
				var isDeleted = await _productDbService.DeleteProductByIDAsync(id);

				if (!isDeleted)
				{
					return NotFound($"No se encontró el usuario con el email: {id}");
				}

				return Ok(new {message = $"Usuario con email {id} eliminado correctamente."});
			}
			catch (Exception ex)
			{
				return BadRequest($"Hubo un problema al eliminar el usuario: {ex.Message}");
			}
		}


		/// <summary>
		/// ENDPOINT Creacion de productos
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("CreateProduct")]
		public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
		{
			try
			{
				// Verificar que los campos necesarios no sean nulos
				if (product == null || string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Description) || product.amount <= 0 || string.IsNullOrEmpty(product.category) || string.IsNullOrEmpty(product.Foto) || string.IsNullOrEmpty(product.price))
				{
					return BadRequest("Todos los campos son requeridos");
				}

				// Llamar al servicio para crear el usuario
				var isCreated = await _productDbService.CreateProductAsync(product);

				if (isCreated)
				{
					return Ok(new { message = "producto creado exitosamente." });
				}
				else
				{
					return BadRequest("Hubo un problema al crear el producto.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Ocurrió un error al crear el producto: " + ex.Message);
			}
		}


		/// <summary>
		/// ENDPOINT ACTUALIZAR Producto
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatedUser"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("UpdateProduct/{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] ProductModel updatedProduct)
		{
			try
			{
				// Llamar al servicio para actualizar el usuario
				var isUpdated = await _productDbService.UpdateProductAsync(id, updatedProduct);

				// Si el usuario no fue encontrado, retornar NotFound
				if (!isUpdated)
				{
					return NotFound($"No se encontró el producto con el ID {id}");
				}

				// Si todo salió bien, retornar Ok
				return Ok(new { message = $"producto con ID {id} actualizado correctamente." });
			}
			catch (Exception ex)
			{
				// En caso de error, retornar BadRequest con el mensaje de la excepción
				return BadRequest($"Hubo un problema al actualizar el producto: {ex.Message}");
			}
		}

	}
}
