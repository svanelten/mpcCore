namespace MpcCore.Extensions
{
	public static class BoolExtensions
	{
		public static string GetParamString(this bool? val)
		{
			if (!val.HasValue)
			{
				return string.Empty;
			}

			return val.Value ? "1" : "0";
		}

		public static string GetParamString(this bool val)
		{
			return val ? "1" : "0";
		}

		public static string GetDisplayString(this bool val)
		{
			return val ? "ON" : "OFF";
		}
	}
}
