using EnviromentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnviromentCrime.Components
{
	public class RoleCheckerViewComponent : ViewComponent
	{
		private readonly IerrandsRepository repository;

		public RoleCheckerViewComponent(IerrandsRepository repository)
		{
			this.repository = repository;
		}

		//Kollar genom så att rätt roll är angiven 
		public IViewComponentResult Invoke()
		{
			ViewBag.User = "";
			if (User.IsInRole("Manager"))
			{
				ViewBag.User = "avdelningschef";
			}
			if (User.IsInRole("Coordinator"))
			{
				ViewBag.User = "samordnare";
			}
			else
			{
				ViewBag.User = "handläggare";
			}

			return View("RoleChecker");
		}
	}
}


