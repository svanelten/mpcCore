using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Options
{
	/// <summary>
	/// Gets volume. If there is no SW mixer, the result will be null.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#playback-options"/>
	/// </summary>
	public class GetVolume : IMpcCoreCommand<int?>
	{
		/// <summary>
		/// Internal command string
		/// </summary>
		public string Command { get; internal set; } = "getvol";

		public int? HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);
			return parser.GetVolume();
		}
	}
}
