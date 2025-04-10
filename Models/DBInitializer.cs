﻿

namespace EnviromentCrime.Models
{
	public class DBInitializer
	{
		public static void EnsurePopulated(IServiceProvider services)
		{
			var context = services.GetRequiredService<ApplicationDbContext>();

			if (!context.Departments.Any())
			{
				context.Departments.AddRange(
				new Department
				{
					DepartmentId = "D01",
					DepartmentName =
				"Tekniska Avloppshanteringen"
				},
				new Department
				{
					DepartmentId = "D02",
					DepartmentName = "Klimat och Energi"
				},
				new Department
				{
					DepartmentId = "D03",
					DepartmentName = "Miljö	och Hälsoskydd"
				},
				new Department
				{
					DepartmentId = "D04",
					DepartmentName = "Natur och Skogsvård"
				},
				new Department
				{
					DepartmentId = "D05",
					DepartmentName = "Renhållning och Avfall"
				}
				);
				context.SaveChanges(); //spara tabellen
			}
			//Kontrollerar om tabellen ErrandStatuses har något innehåll
			if (!context.ErrandStatuss.Any())
			{
				context.ErrandStatuss.AddRange(
				new ErrandStatus
				{
					StatusId = "S_A",
					StatusName =
				"Inrapporterad"
				},
				new ErrandStatus
				{
					StatusId = "S_B",
					StatusName = "Ingen åtgärd"
				},
				new ErrandStatus { StatusId = "S_C", StatusName = "Påbörjad" },
				new ErrandStatus { StatusId = "S_D", StatusName = "Klar" }
				);
				context.SaveChanges();
			}
			//Kontrollerar om tabellen Sequences har något innehåll
			if (!context.Sequences.Any())
			{
				context.Sequences.Add(
				new Sequence { CurrentValue = 200 });
				context.SaveChanges();
			}
			//Kontrollerar om tabellen Employees har något innehåll
			if (!context.Employees.Any())
			{
				context.Employees.AddRange(
				new Employee
				{
					EmployeeId = "E001",
					EmployeName = "ÖstenÄrling",
					RoleTitle = "Coordinator",
					DepartmentId = "D00"
				},
				new Employee
				{
					EmployeeId = "E100",
					EmployeName = "Anna	Åkerman",
					RoleTitle = "Manager",
					DepartmentId = "D01"
				},
				new Employee
				{
					EmployeeId = "E101",
					EmployeName = "Fredrik Roos",
					RoleTitle = "Investigator",
					DepartmentId = "D01"
				},
				new Employee
				{
					EmployeeId = "E102",
					EmployeName = "Gösta Qvist",
					RoleTitle = "Investigator",
					DepartmentId = "D01"
				},
				new Employee
				{
					EmployeeId = "E103",
					EmployeName = "Hilda Persson",
					RoleTitle = "Investigator",
					DepartmentId = "D01"
				},
				new Employee
				{
					EmployeeId = "E200",
					EmployeName = "Bengt Viik",
					RoleTitle = "Manager",
					DepartmentId = "D02"
				},
					new Employee
					{
						EmployeeId = "E201",
						EmployeName = "Ivar Oscarsson",
						RoleTitle = "Investigator",
						DepartmentId = "D02"
					},
				new Employee
				{
					EmployeeId = "E202",
					EmployeName = "Jenny Nordström",
					RoleTitle = "Investigator",
					DepartmentId = "D02"
				},
				new Employee
				{
					EmployeeId = "E203",
					EmployeName = "Kurt Mild",
					RoleTitle = "Investigator",
					DepartmentId = "D02"
				},
					new Employee
					{
						EmployeeId = "E300",
						EmployeName = "Cecilia Unosson",
						RoleTitle = "Manager",
						DepartmentId = "D03"
					},
				new Employee
				{
					EmployeeId = "E301",
					EmployeName = "Lena Larsson",
					RoleTitle = "Investigator",
					DepartmentId = "D03"
				},
				new Employee
				{
					EmployeeId = "E302",
					EmployeName = "Martin Kvist",
					RoleTitle = "Investigator",
					DepartmentId = "D03"
				},
				new Employee
				{
					EmployeeId = "E303",
					EmployeName = "Nina Jansson",
					RoleTitle = "Investigator",
					DepartmentId = "D03"
				},
				new Employee
				{
					EmployeeId = "E400",
					EmployeName = "David Trastlund",
					RoleTitle = "Manager",
					DepartmentId = "D04"
				},
				new Employee
				{
					EmployeeId = "E401",
					EmployeName = "Oskar Ivarsson",
					RoleTitle = "Investigator",
					DepartmentId = "D04"
				},
				new Employee
				{
					EmployeeId = "E402",
					EmployeName = "Petra Hermansdotter",
					RoleTitle = "Investigator",
					DepartmentId = "D04"
				},
				new Employee
				{
					EmployeeId = "E403",
					EmployeName = "Rolf Gunnarsson",
					RoleTitle = "Investigator",
					DepartmentId = "D04"
				},
				new Employee
				{
					EmployeeId = "E500",
					EmployeName = "Emma Svanberg",
					RoleTitle = "Manager",
					DepartmentId = "D05"
				},
				new Employee
				{
					EmployeeId = "E501",
					EmployeName = "Susanne Fred",
					RoleTitle = "Investigator",
					DepartmentId = "D05"
				},
				new Employee
				{
					EmployeeId = "E502",
					EmployeName = "Torsten Embjörn",
					RoleTitle = "Investigator",
					DepartmentId = "D05"
				},
				new Employee
				{
					EmployeeId = "E503",
					EmployeName = "Ulla Davidsson",
					RoleTitle = "Investigator",
					DepartmentId = "D05"
				}
				);
				context.SaveChanges();
			}
			//Kontrollerar om tabellen Errands har något innehåll
			if (!context.Errands.Any())
			{
				context.Errands.AddRange(
				new Errand
				{
					RefNumber = "2023-45-195",
					Place = "Skogslunden vid Jensens gård",
					TypeOfCrime = "Sopor",
					DateOfObservation = new DateTime(2023, 04, 24),
					Observation = "Anmälaren var på promeand i skogslunden när hon upptäckte soporna",
					InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a ett brev till Gösta Olsson",
					InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2023 - 05 - 01",
					InformerName = "Ada Bengtsson",
					InformerPhone = "0432 - 5545522",
					StatusId = "S_D",
					DepartmentId = "D05",
					EmployeeId = "E501"
				},
				new Errand
				{
					RefNumber = "2023-45-196",
					Place = "Småstadsjön",
					TypeOfCrime = "Oljeutsläpp",
					DateOfObservation = new DateTime(2023, 04, 29),
					Observation = "Jag såg en oljefläck på vattnet när jag var där för att fiska",
					InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittas",
					InvestigatorAction = "",
					InformerName = "Bengt Svensson",
					InformerPhone = "0432 - 5152255",
					StatusId = "S_B",
					DepartmentId = "D04",
					EmployeeId = "E401"
				},
				new Errand
				{
					RefNumber = "2023-45-197",
					Place = "Ödehuset",
					TypeOfCrime = "Skrot",
					DateOfObservation = new DateTime(2023, 05, 02),
					Observation = "Anmälaren körde förbi ödehuset och upptäcker ett antal bilar och annat skrot",
					InvestigatorInfo = "Undersökning har gjorts och bilder har tagits",
					InvestigatorAction = "",
					InformerName = "Olle Pettersson",
					InformerPhone = "0432 - 5255522",
					StatusId = "S_C",
					DepartmentId = "D03",
					EmployeeId = "E301"
				},
				new Errand
				{
					RefNumber = "2023-45-198",
					Place = "RestaurangKrögaren",
					TypeOfCrime = "Buller",
					DateOfObservation = new DateTime(2023, 06, 04),
					Observation = "Restaurangen hade för högt ljud på så man inte kunde sova",
					InvestigatorInfo = "Bullermätning har gjorts.Man håller sig inom riktvärden",
					InvestigatorAction = "Meddelat restaurangen att tänka på ljudet i fortsättning",
					InformerName = "Roland Jönsson",
					InformerPhone = "0432-5322255",
					StatusId = "S_D",
					DepartmentId = "D03",
					EmployeeId = "E302"
				},
					new Errand
					{
						RefNumber = "2023-45-199",
						Place = "Torget",
						TypeOfCrime = "Klotter",
						DateOfObservation = new DateTime(2023, 07, 10),
						Observation = "Samtliga skräpkorgar och bänkar är nedklottrade",
						InvestigatorInfo = "",
						InvestigatorAction = "",
						InformerName = "Peter Svensson",
						InformerPhone = "0432 - 5322555",
						StatusId = "S_A"
					}
);
				context.SaveChanges();
			}
		}
	}
}
