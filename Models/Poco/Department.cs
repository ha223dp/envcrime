namespace EnviromentCrime.Models
{
	public class Department
	{
		public String DepartmentId { get; set; }

		public String DepartmentName { get; set; }

		internal static object Where(Func<object, bool> value)
		{
			throw new NotImplementedException();
		}
	}
}
