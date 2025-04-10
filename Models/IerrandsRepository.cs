namespace EnviromentCrime.Models
{
	public interface IerrandsRepository
	{
		IQueryable<Department> Departments { get; }

		IQueryable<Employee> Employees { get; }

		IQueryable<Errand> Errands { get; }

		IQueryable<ErrandStatus> ErrandStatuss { get; }

		IQueryable<Sequence> Sequences { get; }

		IQueryable<Sample> Samples { get; }

		IQueryable<Picture> Pictures { get; }

		string SaveErrand(Errand errand);

		Errand GetErrandDetail(int id);


		Department GetDepartmentDetail(string departmentId);

		Employee GetEmployeeDetail(string name);

		ErrandStatus GetErrandStatus(string name);

		void SaveDepartment(int errandId, string departmentId);

		void SaveErrandStatus(int errandId, string StatusId, string information, string events);

		void SaveManager(int errandId, string EmployeeId, bool noAction, string reason);

		void pictureSaved(int ErrandId, string loadImage);

		void SampleSaved(int ErrandId, string loadSample);

		void UpdateInvestigatorInfo(int ErrandId, Errand errand);

		IQueryable<MyErrand> GetMyErrands();

		IQueryable<MyErrand> GetMyInvestigatorErrands(string investigator);

		IQueryable<MyErrand> GetMyManagerErrands(string managerId);

		IQueryable<Employee> GetEmployees(string employeeDetailId);





	}

}
