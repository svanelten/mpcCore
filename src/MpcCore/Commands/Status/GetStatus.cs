using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Status
{
	/// <summary>
	/// Reports the current status of the player and the volume level
	/// </summary>
	public class GetStatus : IMpcCoreCommand<IStatus>
	{
		/// <summary>
		/// Internal command string
		/// </summary>
		public string Command { get; internal set; } = "status";

		public IStatus HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			return parser.GetStatus();
		}
	}
}
