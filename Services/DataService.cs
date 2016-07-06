using System.Web.Mvc;
using System.Collections.Generic;
using CodeEffects.Rule.Common;
using CodeEffects.Rule.Demo.Bre.Mvc.Models;

namespace CodeEffects.Rule.Demo.Bre.Mvc.Services
{
	public class DataService
	{
		public static List<SelectListItem> GetGroups(bool valueAsIndex)
		{
			List<SelectListItem> groups = new List<SelectListItem>();
			groups.Add(new SelectListItem { Selected = true, Text = "With Dicsount", Value = valueAsIndex ? "1" : "With Dicsount" });
			groups.Add(new SelectListItem { Text = "Without Dicsount", Value = valueAsIndex ? "0" : "With Dicsount" });
			return groups;
		}

		public static List<SelectListItem> GetStates(bool valueAsIndex)
		{
			List<SelectListItem> states = new List<SelectListItem>();
			states.Add(new SelectListItem { Selected = true, Text = State.Arizona.ToString(), Value = valueAsIndex ? "0" : State.Arizona.ToString() });
			states.Add(new SelectListItem { Text = State.California.ToString(), Value = valueAsIndex ? "1" : State.California.ToString() });
			states.Add(new SelectListItem { Text = State.Florida.ToString(), Value = valueAsIndex ? "2" : State.Florida.ToString() });
			states.Add(new SelectListItem { Text = State.NorthCarolina.ToString(), Value = valueAsIndex ? "3" : State.NorthCarolina.ToString() });
			states.Add(new SelectListItem { Text = State.Georgia.ToString(), Value = valueAsIndex ? "4" : State.Georgia.ToString() });
			return states;
		}

		public static List<SelectListItem> GetEducationLevels()
		{
			List<SelectListItem> educationLevels = new List<SelectListItem>();
			educationLevels.Add(new SelectListItem { Selected = true, Text = "Master Degree", Value = "0" });
			educationLevels.Add(new SelectListItem { Text = "College", Value = "1" });
			educationLevels.Add(new SelectListItem { Text = "High School", Value = "2" });
			educationLevels.Add(new SelectListItem { Text = "Other", Value = "3" });
			return educationLevels;
		}

		public static List<SelectListItem> GetPhysicians()
		{
			int i = 0;
			List<SelectListItem> physicians = new List<SelectListItem>();
			foreach(DataSourceItem item in Physician.List())
				physicians.Add(Convert(item, ++i == 0));
			return physicians;
		}

		private static SelectListItem Convert(DataSourceItem item, bool selected)
		{
			return new SelectListItem { Text = item.Name, Value = item.ID.ToString(), Selected = selected };
		}
	}
}