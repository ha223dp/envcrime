using Microsoft.EntityFrameworkCore;

namespace EnviromentCrime.Models
{
	public class EfDatabaseRepository : IerrandsRepository
	{
		private readonly ApplicationDbContext context;


		private IWebHostEnvironment webHostEnvironment;

		private IHttpContextAccessor contextAcc;

		public EfDatabaseRepository(ApplicationDbContext ctx, IHttpContextAccessor cont)
		{
			context = ctx;
			contextAcc = cont;
			var userName = contextAcc.HttpContext.User.Identity.Name;
		}

		public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);
		public IQueryable<Department> Departments => context.Departments;
		public IQueryable<Employee> Employees => context.Employees;
		public IQueryable<ErrandStatus> ErrandStatuss => context.ErrandStatuss;

		public IQueryable<Sequence> Sequences => context.Sequences;

		public IQueryable<Sample> Samples => context.Samples;

		public IQueryable<Picture> Pictures => context.Pictures;



		public Errand GetErrandDetail(int id)
		{
			var errandDetail = Errands.Where(st => st.ErrandId == id).First();
			return errandDetail;
		}

		//kollar om rätt sparade av ärdenet är korrekt.
		public string SaveErrand(Errand errand)
		{
			if (errand.ErrandId != null)
			{
				var currentValue = Sequences.Where(s => s.Id == 1).FirstOrDefault();

				errand.RefNumber = DateTime.Now.Year + "-45 -" + currentValue.CurrentValue; ;
				errand.StatusId = "S_A";

				context.Errands.Add(errand);
				currentValue.CurrentValue++;
				context.SaveChanges();
			}
			else
			{
				Errand dbEntry = context.Errands.FirstOrDefault(sd => sd.ErrandId == errand.ErrandId);
				if (dbEntry != null)
				{
					dbEntry.ErrandId = errand.ErrandId;
					dbEntry.RefNumber = errand.RefNumber;
					dbEntry.Place = errand.Place;
					dbEntry.TypeOfCrime = errand.TypeOfCrime;
					dbEntry.DateOfObservation = errand.DateOfObservation;
					dbEntry.InformerName = errand.InformerName;
					dbEntry.InformerPhone = errand.InformerPhone;
					dbEntry.Observation = errand.Observation;

				}
			}
			context.SaveChanges();
			return errand.RefNumber;
		}



		public void UpdateErrand(Errand errand)
		{
			Errand dbEntry = context.Errands.FirstOrDefault(sd => sd.ErrandId == errand.ErrandId);
			if (dbEntry != null)
			{
				dbEntry.ErrandId = errand.ErrandId;
			}
			context.SaveChanges();
		}

		public Errand DeleteErrand(Errand errand)
		{
			Errand dbEntry = context.Errands.FirstOrDefault(sd => sd.ErrandId == errand.ErrandId);
			if (dbEntry != null)
			{
				context.Errands.Remove(dbEntry);
			}
			context.SaveChanges();
			return dbEntry;

		}

		public void SaveDepartment(int errandid, string departmentId)
		{
			Errand dbEntry = context.Errands.FirstOrDefault(sd => sd.ErrandId == errandid);
			if (dbEntry != null)
			{
				dbEntry.DepartmentId = departmentId;
			}
			context.SaveChanges();

		}

		public void SaveErrandStatus(int errandid, string StatusId, string information, string events)
		{
			Errand dbEntry = context.Errands.FirstOrDefault(sd => sd.ErrandId == errandid);
			if (dbEntry != null)
			{
				if (StatusId != null)
				{
					dbEntry.StatusId = StatusId;
				}

				if (information != null)
				{
					dbEntry.InvestigatorInfo += ": " + information;
				}

				if (events != null)
				{
					dbEntry.InvestigatorAction += ": " + events;
				}

			}
			context.SaveChanges();

		}



		public void SaveManager(int errandid, string EmployeeId, bool noAction, string reason)
		{
			Errand dbEntry = context.Errands.FirstOrDefault(sd => sd.ErrandId == errandid);
			if (dbEntry != null)
			{
				if (noAction == true)
				{
					dbEntry.InvestigatorInfo = reason;
					dbEntry.StatusId = "S_B";
				}

				else
				{
					noAction = false;
					dbEntry.EmployeeId = EmployeeId;
				}
				context.SaveChanges();
			}

		}

		//sparar de bilder som skickas
		public void pictureSaved(int errandid, string loadImage)
		{
			Picture picture = new Picture();
			picture.PictureName = loadImage;
			picture.ErrandId = errandid;

			context.Pictures.Add(picture);
			context.SaveChanges();
		}
		//sparar de filer som skickas
		public void SampleSaved(int errandid, string loadSample)
		{
			Sample sample = new Sample();
			sample.SampleName = loadSample;
			sample.ErrandId = errandid;

			context.Samples.Add(sample);
			context.SaveChanges();
		}

		public void UpdateInvestigatorInfo(int ErrandId, Errand errand)
		{
			Errand dbEntry = GetErrandDetail(ErrandId);

			if (errand.InvestigatorInfo != null)
			{
				dbEntry.InvestigatorInfo = dbEntry.InvestigatorInfo + "Notering" + errand.InvestigatorInfo;


			}

			if (errand.InvestigatorAction != null)
			{
				dbEntry.InvestigatorAction = dbEntry.InvestigatorAction + "Notering" + errand.InvestigatorAction;
			}

			if (errand.StatusId != "Välj")
			{
				dbEntry.StatusId = errand.StatusId;
			}

			context.SaveChanges();

		}

		public void UpdateDepartmentInfo(string DepartmentId, Department department)
		{
			Department dbEntry = GetDepartmentDetail(DepartmentId);

			if (department.DepartmentId != null)
			{
				dbEntry.DepartmentId = dbEntry.DepartmentId + department.DepartmentId;


			}

			context.SaveChanges();

		}

		public void UpdateEmployeeInfo(string EmployeeId, Employee employee)
		{
			Employee dbEntry = GetEmployeeDetail(EmployeeId);

			if (employee.EmployeeId != null)
			{
				dbEntry.EmployeeId = dbEntry.EmployeeId + employee.EmployeeId;
			}

			context.SaveChanges();

		}

		public void UpdateErrandStatusInfo(string StatusId, ErrandStatus errandStatus)
		{
			ErrandStatus dbEntry = GetErrandStatus(StatusId);

			if (errandStatus.StatusId != null)
			{
				dbEntry.StatusId = dbEntry.StatusId + errandStatus.StatusId;
			}

			context.SaveChanges();

		}

		//filterar de ärden
		public IQueryable<MyErrand> GetMyErrands()
		{
			var errandList = from err in Errands
							 join stat in ErrandStatuss on err.StatusId equals stat.StatusId
							 join dep in Departments on err.DepartmentId equals dep.DepartmentId
							 into departmentErrand
							 from deptE in departmentErrand.DefaultIfEmpty()
							 join em in Employees on err.EmployeeId equals em.EmployeeId
							 into employeeErrand
							 from empE in employeeErrand.DefaultIfEmpty()
							 orderby err.RefNumber ascending
							 select new MyErrand
							 {
								 DateOfObservation = err.DateOfObservation,
								 ErrandId = err.ErrandId,
								 RefNumber = err.RefNumber,
								 TypeOfCrime = err.TypeOfCrime,
								 StatusName = stat.StatusName,
								 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
								 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeName)
							 };

			return errandList;
		}

		//kollar om rätt errand till rätt employed id är korrekt till investigator
		public IQueryable<MyErrand> GetMyInvestigatorErrands(string investigator)
		{
			var errandList = from err in Errands
							 join stat in ErrandStatuss on err.StatusId equals stat.StatusId
							 join dep in Departments on err.DepartmentId equals dep.DepartmentId
							 into departmentErrand
							 from deptE in departmentErrand.DefaultIfEmpty()
							 join em in Employees on err.EmployeeId equals em.EmployeeId
							 into employeeErrand
							 from empE in employeeErrand.DefaultIfEmpty()
							 orderby err.RefNumber ascending
							 where err.EmployeeId == investigator
							 select new MyErrand
							 {
								 DateOfObservation = err.DateOfObservation,
								 ErrandId = err.ErrandId,
								 RefNumber = err.RefNumber,
								 TypeOfCrime = err.TypeOfCrime,
								 StatusName = stat.StatusName,
								 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
								 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeName)
							 };
			return errandList;
		}

		//kollar om rätt ärde från departmentid är korrekt insatt på rätt employee departmentid
		public IQueryable<MyErrand> GetMyManagerErrands(string managerId)
		{
			Employee employee = GetEmployeeDetail(managerId);
			//ska lägga en employee här för att hämta från departmentid
			var errandList = from err in Errands
							 join stat in ErrandStatuss on err.StatusId equals stat.StatusId
							 join dep in Departments on err.DepartmentId equals dep.DepartmentId
							 into departmentErrand
							 from deptE in departmentErrand.DefaultIfEmpty()
							 join em in Employees on err.EmployeeId equals em.EmployeeId
							 into employeeErrand
							 from empE in employeeErrand.DefaultIfEmpty()
							 orderby err.RefNumber ascending
							 where err.DepartmentId == employee.DepartmentId
							 select new MyErrand
							 {
								 DateOfObservation = err.DateOfObservation,
								 ErrandId = err.ErrandId,
								 RefNumber = err.RefNumber,
								 TypeOfCrime = err.TypeOfCrime,
								 StatusName = stat.StatusName,
								 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
								 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeName)
							 };
			return errandList;
		}


		public IQueryable<Employee> GetEmployees(string employeeDetailId)
		{
			var deptId = GetEmployeeDetail(employeeDetailId).DepartmentId;
			var employeeList = context.Employees.Where(st => st.DepartmentId == deptId);
			return employeeList;
		}

		public IQueryable<Employee> GetCrimeEmployees(string crimeEmployeeId)
		{
			var deptId = GetDepartmentDetail(crimeEmployeeId).DepartmentId;
			var employeeList = context.Employees.Where(st => st.DepartmentId == deptId);
			return employeeList;
		}


		public Department GetDepartmentDetail(string departmentId)
		{
			var DepartmentDetail = Departments.Where(st => st.DepartmentId == departmentId).First();
			return DepartmentDetail;
		}

		public Employee GetEmployeeDetail(string id)
		{
			var EmployeeDetail = context.Employees.Where(st => st.EmployeeId == id).First();
			return EmployeeDetail;
		}

		public ErrandStatus GetErrandStatus(string id)
		{
			var StatusDetail = context.ErrandStatuss.Where(st => st.StatusId == id).First();
			return StatusDetail;
		}




	}
}