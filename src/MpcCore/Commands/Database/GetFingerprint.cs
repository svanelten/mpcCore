using MpcCore.Contracts;
using MpcCore.Response;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Calculate the items audio fingerprint.
	/// This command is only available if MPD was built with libchromaprint (-Dchromaprint=enabled).
	/// Will return an empty string if no fingerprint is available.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class GetFingerprint : IMpcCoreCommand<string>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">item path</param>
		public GetFingerprint(string path)
		{
			Command = $"getfingerprint \"{path}\"";
		}

		public string HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetFingerprint();
		}
	}
}
