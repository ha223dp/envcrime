using System.ComponentModel.DataAnnotations;



namespace EnviromentCrime.Models
{
	public class Errand
	{
		[Key]
		public int ErrandId { get; set; }
		public string RefNumber { get; set; }

		[Display(Name = "Vart har brottet skett någonstans?")]
		[Required(ErrorMessage = "Du måste ha skrivit in ett brott")]

		public String Place { get; set; }

		[Display(Name = "Vilket slags brott har inträffat?")]
		[Required(ErrorMessage = "Du måste ha skrivit in vilken slags brott")]
		public String TypeOfCrime { get; set; }

		[Display(Name = "Berätta när det har inträffat?")]
		[Required(ErrorMessage = "Du måste välja datum")]
		[DataType(DataType.Date)]
		public DateTime DateOfObservation { get; set; }

		[Display(Name = "Fullständigt namn!")]
		[Required(ErrorMessage = "Du måste fylla i namnet")]
		public String InformerName { get; set; }

		[Display(Name = "Fullständigt nummer!")]
		[RegularExpression(pattern: @"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$", ErrorMessage = "Formatet för mobilnummer ska vara xxxx-xxxxxxxxx")]
		[Required(ErrorMessage = "Du måste fylla in ditt nummert")]
		public String InformerPhone { get; set; }

		[Display(Name = "Skriv din observation av händelsen: (ex. namn på misstänkt person)")]
		public String Observation { get; set; }

		public String InvestigatorInfo { get; set; }
		public String InvestigatorAction { get; set; }
		public String StatusId { get; set; }
		public String DepartmentId { get; set; }

		public String EmployeeId { get; set; }

		public ICollection<Sample> Samples { get; set; }
		public ICollection<Picture> Pictures { get; set; }


		internal static object Where(Func<object, bool> value)
		{
			throw new NotImplementedException();
		}


	}
}



