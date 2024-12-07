using System.ComponentModel.DataAnnotations.Schema;

namespace PRUEBA_TECNICA.Models
{
	[Table(name: "USERS", Schema = "PRD")]
	public class UserModel
	{
		public int Id { get; set; }	

		public string Name { get; set; }

		public string email { get; set; }

		public string password { get; set; }

		public string rol { get; set; }
	}
}
