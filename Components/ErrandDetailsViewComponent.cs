using EnviromentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnviromentCrime.Components
{
	public class ErrandDetailsViewComponent : ViewComponent
	{
		private readonly IerrandsRepository repository;

		public ErrandDetailsViewComponent(IerrandsRepository repository)
		{
			this.repository = repository;
		}
		// att rätt information kommer up på rätt ställe i var sin respektive Get metod.
		//och returnerar det som håller fast med det.
		public Microsoft.AspNetCore.Mvc.IViewComponentResult Invoke(int id)
		{
			var errandDetails = repository.GetErrandDetail(id);
			if (errandDetails.DepartmentId != null)
			{
				ViewBag.dept = repository.GetDepartmentDetail(errandDetails.DepartmentId).DepartmentName;
			}
			if (errandDetails.EmployeeId != null)
			{
				ViewBag.employee = repository.GetEmployeeDetail(errandDetails.EmployeeId).EmployeName;
			}
			if (errandDetails.StatusId != null)
			{
				ViewBag.status = repository.GetErrandStatus(errandDetails.StatusId).StatusName;
			}


			return View("ItemList", errandDetails);
		}
	}
}
