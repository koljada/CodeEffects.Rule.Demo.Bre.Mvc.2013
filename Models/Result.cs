
namespace CodeEffects.Rule.Demo.Bre.Mvc.Models
{
	public class Result
	{
		public bool IsRuleEmpty { get; set; }
		public bool IsRuleValid { get; set; }
		public string Output { get; set; }
		public string ClientInvalidData { get; set; }
		public Patient Patient { get; set; }

		public Result()
		{
			this.IsRuleEmpty = false;
			this.IsRuleValid = true;
		}
	}
}