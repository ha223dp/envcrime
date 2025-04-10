using EnviromentCrime.Infrastructure;
using EnviromentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Enviromentcrime.Controllers
{

	public class HomeController : Controller
	{

		private readonly IerrandsRepository repository;
		private UserManager<IdentityUser> userManager;
		private SignInManager<IdentityUser> signInManager;

		public HomeController(IerrandsRepository repo, UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
		{
			repository = repo;
			userManager = userMgr;
			signInManager = signInMgr;
		}



		public ViewResult Index()
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

		//kollar med nästa metod om det funkar eller ej
		public ViewResult Login(string returnUrl)
		{

			return View(new LoginModel { ReturnUrl = returnUrl });
		}


		//Kollar om rätt angivna roll är rätt uppsatt med lösenord och användarnamn
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);
				if (user != null)
				{
					await signInManager.SignOutAsync();
					if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
					{
						if (await userManager.IsInRoleAsync(user, "Coordinator"))
						{
							return Redirect("/Coordinator/StartCoordinator");
						}
						if (await userManager.IsInRoleAsync(user, "Investigator"))
						{
							return Redirect("/Investigator/StartInvestigator");
						}
						if (await userManager.IsInRoleAsync(user, "Manager"))
						{
							return Redirect("/Manager/StartManager");
						}
						//return Redirect("/Home/Login");
					}



				}
			}
			ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
			return View(loginModel);
		}


		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			await signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}

		[AllowAnonymous]
		public ViewResult AccessDenied()
		{
			return View(Index());
		}

	}


}