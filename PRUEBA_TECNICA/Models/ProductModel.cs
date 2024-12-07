using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PRUEBA_TECNICA.Models
{
	[Table(name: "Products", Schema = "PRD")]
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }	

		public string Foto { get; set; }

		public string category {  get; set; }

		public string amount { get; set; }

		public string price { get; set; }
	}
}
