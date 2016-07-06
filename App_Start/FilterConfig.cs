using System.Web;
using System.Web.Mvc;

namespace CodeEffects.Rule.Demo.Bre.Mvc
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
