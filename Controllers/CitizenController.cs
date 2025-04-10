using EnviromentCrime.Infrastructure;
using EnviromentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Enviromentcrime.Controllers
{
	public class CitizenController : Controller
	{
		private readonly IerrandsRepository repository;

		public CitizenController(IerrandsRepository repo)
		{
			repository = repo;
		}

		public ViewResult Contact() { return View(); }
		public ViewResult Faq() { return View(); }
		public ViewResult Services() { return View(); }

		//Kollar genom om rätt refnummer kommer till rätt plats
		public ViewResult Thanks()
		{
			var myLoadingErrand = HttpContext.Session.Get<Errand>("NewErrand");
			ViewBag.Message = HttpContext.Session.GetString("NewErrand");

			ViewBag.RefNumber = repository.SaveErrand(myLoadingErrand);
			HttpContext.Session.Remove("NewErrand");
			HttpContext.Session.Clear();
			return View();

		}

		//Kollar om det går att validera olika objekt
		[HttpPost]
		public ViewResult Validate(Errand errand)
		{
			HttpContext.Session.Set<Errand>("NewErrand", errand);
			return View(errand);
		}
	}
}
