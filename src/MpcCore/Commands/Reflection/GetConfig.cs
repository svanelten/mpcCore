using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Reflection
{
	/// <summary>
	/// Dumps configuration values that may be interesting for the client. This command is only permitted to “local” clients (connected via local socket).
	/// The following response attributes are available: "music_directory"
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#reflection"/>
	/// </summary>
	public class GetConfig : IMpcCoreCommand<IEnumerable<KeyValuePair<string,string>>>
	{
		/// <summary>
		/// The internal command string
		/// </summary>
		public string Command { get; internal set; } = "config";

		public IEnumerable<KeyValuePair<string, string>> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetKeyValueList();
		}
	}
}
