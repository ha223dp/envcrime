using EnviromentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enviromentcrime.Controllers
{
	[Authorize(Roles = "Manager")]
	public class ManagerController : Controller
	{
		private IHttpContextAccessor contextAcc;

		private readonly IerrandsRepository repository;

		public ManagerController(IerrandsRepository repo, IHttpContextAccessor cont)
		{
			repository = repo;
			contextAcc = cont;
		}

		//kollar om idet är korrekt
		public ViewResult CrimeManager(int id)
		{

			var ManagerName = contextAcc.HttpContext.User.Identity.Name;
			TempData["ID"] = id;
			ViewBag.Id = id;
			ViewBag.ListOfEmployeeId = repository.GetEmployees(ManagerName);

			return View();
		}

		//kollar om rätt namn på handläggaren är angiven
		public ViewResult StartManager()
		{

			ViewBag.User = contextAcc.HttpContext.User.Identity.Name;
			var ManagerName = contextAcc.HttpContext.User.Identity.Name;
			ViewBag.ManagerName = ManagerName;
			return View(repository);
		}


		//kollar om i fyllnaden är korrekt angiven och i tryckt
		public IActionResult SaveManager(Employee employee, bool noAction, string reason)
		{
			int someId = int.Parse(TempData["ID"].ToString());

			if (employee.EmployeeId == null && noAction == false)
			{
				return RedirectToAction("CrimeManager", new { id = someId });
			}

			repository.SaveManager(someId, employee.EmployeeId, noAction, reason);

			return RedirectToAction("CrimeManager", new { id = someId });
		}



	}
}
