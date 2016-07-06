using CodeEffects.Rule.Attributes;

namespace CodeEffects.Rule.Demo.Bre.Mvc.Models
{
	public enum State
	{
		Arizona,
		California,
		Florida,
		[EnumItem("North Carolina")]
		NorthCarolina,
		Georgia,
		[ExcludeFromEvaluation]
		Unknown
	}
}