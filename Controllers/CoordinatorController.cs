using EnviromentCrime.Infrastructure;
using EnviromentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enviromentcrime.Controllers
{
	[Authorize(Roles = "Coordinator")]
	public class CoordinatorController : Controller
	{

		private readonly IerrandsRepository repository;
		private IHttpContextAccessor contextAcc;

		public CoordinatorController(IerrandsRepository repo, IHttpContextAccessor contextAcc)
		{
			repository = repo;
			this.contextAcc = contextAcc;
		}
		//Kollar om rätt angivna id kommer till rätt plats från repository
		public ViewResult CrimeCoordinator(int id)
		{
			TempData["ID"] = id;
			ViewBag.Id = id;
			ViewBag.ListOfId = repository.Departments;
			return View();
		}

		//kollar om det som är ifylld är korrekt, samt att det visas på itemlist sidan
		public ViewResult ReportCrime()
		{
			var myLoadingErrand = HttpContext.Session.Get<Errand>("NewErrand");
			if (myLoadingErrand == null)
			{
				return View();
			}
			else
			{
				return View(myLoadingErrand);
			}

		}
		//tar in repository och även anläggarens namn
		public ViewResult StartCoordinator()
		{
			ViewBag.User = contextAcc.HttpContext.User.Identity.Name;
			return View(repository);
		}
		//Kollar om det som sparas är korrekt
		public ViewResult Thanks()
		{
			var myLoadingErrand = HttpContext.Session.Get<Errand>("NewErrand");
			ViewBag.Message = HttpContext.Session.GetString("NewErrand");

			ViewBag.RefNumber = repository.SaveErrand(myLoadingErrand);
			HttpContext.Session.Remove("NewErrand");
			HttpContext.Session.Clear();
			return View();
		}


		[HttpPost]
		public ViewResult Validate(Errand errand)
		{
			HttpContext.Session.Set<Errand>("NewErrand", errand);
			return View(errand);
		}

		public IActionResult SaveDepartment(Department department)
		{
			int someId = int.Parse(TempData["ID"].ToString());


			if (department.DepartmentId == "D00")
			{
				return RedirectToAction("CrimeCoordinator", new { id = someId });
			}

			repository.SaveDepartment(someId, department.DepartmentId);



			return RedirectToAction("CrimeCoordinator", new { id = someId });
		}


		[AllowAnonymous]
		public ViewResult AccessDenied() { return View(); }

	}
}
