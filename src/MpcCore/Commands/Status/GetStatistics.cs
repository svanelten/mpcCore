using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Status
{
	/// <summary>
	/// Gets the current mpd statistics
	/// </summary>
	public class GetStatistics : IMpcCoreCommand<IStatistics>
	{
		/// <summary>
		/// Internal command string
		/// </summary>
		public string Command { get; internal set; } = "stats";

		public IStatistics HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetStatistics();
		}
	}
}
