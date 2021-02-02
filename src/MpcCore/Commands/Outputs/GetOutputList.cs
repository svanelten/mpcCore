using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Outputs
{
	/// <summary>
	/// Returns a list of all outputdevices available
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#audio-output-devices"/>
	/// </summary>
	public class GetOutputDeviceList : IMpcCoreCommand<IEnumerable<IOutputDevice>>
	{
		/// <summary>
		/// The internal command string
		/// </summary>
		public string Command { get; internal set; } = "outputs";

		public IEnumerable<IOutputDevice> HandleResponse(IMpdResponse response)
		{ 
			var parser = new ResponseParser(response);

			return parser.GetListedOutputDevices();
		}
	}
}
