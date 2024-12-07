using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRUEBA_TECNICA.Models
{
	[Table(name: "CATEGORYS", Schema ="PRD")]
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } 

	}
}
