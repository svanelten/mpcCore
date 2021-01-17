using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Calculate the songs audio fingerprint.
	/// This command is only available if MPD was built with libchromaprint (-Dchromaprint=enabled).
	/// Will return an empty string if no fingerprint is available.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class GetFingerprint : IMpcCoreCommand<string>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Calculate the songs audio fingerprint.
		/// This command is only available if MPD was built with libchromaprint (-Dchromaprint=enabled).
		/// Will return an empty string if no fingerprint is available.
		/// </summary>
		/// <param name="uri">item path</param>
		public GetFingerprint(string uri)
		{
			Command = $"getfingerprint \"{uri}\"";
		}

		public string HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			return parser.GetFingerprint();
		}
	}
}
