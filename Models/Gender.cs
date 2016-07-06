using CodeEffects.Rule.Attributes;

namespace CodeEffects.Rule.Demo.Bre.Mvc.Models
{
	public enum Gender
	{
		Male,
		Female,
		[ExcludeFromEvaluation]
		Unknown
	}
}