namespace MpcCore.Extensions
{
	public static class IntExtensions
	{
		public static string GetParamString(this int? val, string defaultValue = "")
		{
			return val.HasValue ? val.Value.ToString() : defaultValue;
		}
	}
}
