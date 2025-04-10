using System.ComponentModel.DataAnnotations;

namespace EnviromentCrime.Models
{
	public class ErrandStatus
	{
		[Key]
		public String StatusId { get; set; }

		public String StatusName { get; set; }

	}
}
