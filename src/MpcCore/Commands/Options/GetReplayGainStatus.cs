using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Prints replay gain options. Currently, only the variable replay_gain_mode is returned.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class GetReplayGainStatus : IMpcCoreCommand<string>
	{
		public string Command { get; internal set; } = "replay_gain_status";

		public string HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetReplayGainStatus();
		}
	}
}
