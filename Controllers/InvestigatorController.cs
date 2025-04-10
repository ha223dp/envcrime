using EnviromentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enviromentcrime.Controllers
{
	[Authorize(Roles = "Investigator")]
	public class InvestigatorController : Controller
	{

		private readonly IerrandsRepository repository;

		private readonly IWebHostEnvironment webHostEnvironment;

		private IHttpContextAccessor contextAcc;
		public InvestigatorController(IerrandsRepository repo, IWebHostEnvironment enviroment, IHttpContextAccessor cont)
		{
			repository = repo;
			webHostEnvironment = enviroment;
			contextAcc = cont;

		}

		//kollar om rätt id är rätt uppkopplade
		public ViewResult CrimeInvestigator(int id)
		{

			TempData["ID"] = id;
			ViewBag.Id = id;
			ViewBag.ErrandStatusId = repository.ErrandStatuss;
			return View();
		}
		//kollar om userName är korrekt angiven
		public ViewResult StartInvestigator()
		{
			ViewBag.User = contextAcc.HttpContext.User.Identity.Name;
			var userName = contextAcc.HttpContext.User.Identity.Name;
			ViewBag.UserName = userName;
			return View(repository);
		}


		//Om respektive objekt i parametern är ifylld under statusuppdateringen
		public async Task<IActionResult> SaveErrandStatus(ErrandStatus errandStatus, string information, string events, IFormFile loadSample, IFormFile loadImage)
		{
			int errandId = int.Parse(TempData["ID"].ToString());

			if (errandStatus.StatusId == "Välj" && information == null && events == null && loadSample == null && loadImage == null)
			{
				return RedirectToAction("CrimeInvestigator", new { id = errandId });
			}

			repository.SaveErrandStatus(errandId, errandStatus.StatusId, information, events);


			await UploadFiles(errandId, loadSample);
			await UploadImages(errandId, loadImage);


			return RedirectToAction("CrimeInvestigator", new { id = errandId });
		}

		//kollar om det går att ladda upp filer
		public async Task UploadFiles(int errandId, IFormFile loadSample)
		{
			var tempPath = Path.GetTempFileName();
			if (loadSample.Length > 0)
			{
				using (var stream = new FileStream(tempPath, FileMode.Create))
				{
					await loadSample.CopyToAsync(stream);
				}
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadSample.FileName;

				var path = Path.Combine(webHostEnvironment.WebRootPath, "Documents", uniqueFileName);

				System.IO.File.Move(tempPath, path);
				repository.SampleSaved(errandId, uniqueFileName);
			}
		}

		//går att ladda upp bilder
		public async Task UploadImages(int errandId, IFormFile loadImage)
		{
			var tempPaths = Path.GetTempFileName();
			if (loadImage.Length > 0)
			{
				using (var stream = new FileStream(tempPaths, FileMode.Create))
				{
					await loadImage.CopyToAsync(stream);
				}
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadImage.FileName;

				var path = Path.Combine(webHostEnvironment.WebRootPath, "Documents", uniqueFileName);
				System.IO.File.Move(tempPaths, path);
				repository.pictureSaved(errandId, uniqueFileName);
			}
		}




	}

}

