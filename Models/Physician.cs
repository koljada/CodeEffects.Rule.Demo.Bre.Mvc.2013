using System.Collections.Generic;

namespace CodeEffects.Rule.Demo.Bre.Mvc.Models
{
	/// <summary>
	/// A supportive class that demonstrates the use of dynamic menu data sources
	/// </summary>
	public class Physician
	{
		/// <summary>
		/// Returns a list of fictitious physicians
		/// </summary>
		/// <returns>List of data source items</returns>
		public static List<Rule.Common.DataSourceItem> List()
		{
			List<Rule.Common.DataSourceItem> people = new List<Rule.Common.DataSourceItem>();

			people.Add(new Rule.Common.DataSourceItem(0, "John Smith"));
			people.Add(new Rule.Common.DataSourceItem(1, "Anna Taylor"));
			people.Add(new Rule.Common.DataSourceItem(2, "Robert Brown"));
			people.Add(new Rule.Common.DataSourceItem(3, "Stephen Lee"));
			people.Add(new Rule.Common.DataSourceItem(4, "Joe Wilson"));
			people.Add(new Rule.Common.DataSourceItem(5, "Samuel Thompson"));

			return people;
		}
	}
}