using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Reflection
{
	/// <summary>
	/// Gets a list of decoder plugins, followed by their supported suffixes and MIME types.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#reflection"/>
	/// </summary>
	public class GetDecoderPlugins : IMpcCoreCommand<IEnumerable<IDecoderPlugin>>
	{
		/// <summary>
		/// The internal command string
		/// </summary>
		public string Command { get; internal set; } = "decoders";

		public IEnumerable<IDecoderPlugin> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetListedDecoderPlugins();
		}
	}
}
