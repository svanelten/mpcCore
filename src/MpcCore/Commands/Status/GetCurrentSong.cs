using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Status
{
	/// <summary>
	/// Displays the info of the current queue item (same item that is identified in status)
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#querying-mpd-s-status"/>
	/// </summary>
	public class GetCurrentSong : IMpcCoreCommand<IItem>
	{
		public string Command { get; internal set; } = "currentsong";

		public IItem HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			if (parser.ResponseHasNoContent)
			{
				return null;
			}
			
			return parser.GetListedTracks().FirstOrDefault();
		}
	}
}
