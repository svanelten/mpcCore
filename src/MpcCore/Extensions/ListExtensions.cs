using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Extensions
{
	public static class ListExtensions
	{
		public static bool IsNullOrEmpty(this List<string> list)
		{
			return (list == null || list.Count == 0);
		}
		public static bool IsBasicOkResponse(this List<string> list)
		{
			return (list?.Count == 1 && list?.First() == Constants.Ok);
		}

		public static bool IsErrorResponse(this List<string> list)
		{
			return list?.Count == 1 && (list.First().StartsWith(Constants.Ack));
		}

		public static bool IsBasicErrorResponse(this List<string> list)
		{
			return (list?.Count == 1 && list?.First() == Constants.Ack);
		}
	}
}